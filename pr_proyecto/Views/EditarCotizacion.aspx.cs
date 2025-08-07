using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using pr_proyecto.Helpers;
using pr_proyecto.Models;
using pr_proyecto.Repository;
using pr_proyecto.Services;

namespace pr_proyecto.Views
{
    public partial class EditarCotizacion : System.Web.UI.Page, IDisposable
    {
        private readonly ICotizacionService _cotizacionService;
        private readonly ICotizacionServicioService _cotizacionServicioService;
        private readonly ISucursalService _sucursalService;
        private readonly IUsuarioService _usuarioService;
        private readonly Data.AppDbContext _sharedContext;
        private Cotizacion _cotizacionActual;
        private int _idCotizacion;

        public EditarCotizacion()
        {
            // Usar un solo contexto compartido para todos los repositorios
            _sharedContext = new Data.AppDbContext();
            
            var cotizacionRepository = new CotizacionRepository(_sharedContext);
            var cotizacionServicioRepository = new CotizacionServicioRepository(_sharedContext);
            var sucursalRepository = new SucursalRepository(_sharedContext);
            var usuarioRepository = new UsuarioRepository(_sharedContext);
            
            _cotizacionService = new CotizacionService(cotizacionRepository, cotizacionServicioRepository);
            _cotizacionServicioService = new CotizacionServicioService(cotizacionServicioRepository);
            _sucursalService = new SucursalService(sucursalRepository);
            _usuarioService = new UsuarioService(usuarioRepository);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar autenticación
            AuthHelper.RequireAuthentication();

            // Obtener ID de cotización
            if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out _idCotizacion))
            {
                if (!IsPostBack)
                {
                    CargarCotizacion();
                    CargarDropDownSucursales();
                }
                else
                {
                    // En postback, recuperar la cotización desde la sesión o cargarla nuevamente
                    RecuperarCotizacion();
                }
            }
            else
            {
                Response.Redirect("~/Views/VerCotizaciones.aspx");
            }
        }

        private void RecuperarCotizacion()
        {
            try
            {
                // Intentar recuperar desde la sesión primero
                _cotizacionActual = Session["CotizacionEditar"] as Cotizacion;
                
                // Si no existe en sesión o el ID no coincide, recargar
                if (_cotizacionActual == null || _cotizacionActual.IdCotizacion != _idCotizacion)
                {
                    _cotizacionActual = _cotizacionService.ObtenerPorId(_idCotizacion);
                    if (_cotizacionActual != null)
                    {
                        // Desconectar la entidad del contexto antes de guardarla en sesión
                        _sharedContext.Entry(_cotizacionActual).State = System.Data.Entity.EntityState.Detached;
                        Session["CotizacionEditar"] = _cotizacionActual;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error recuperando cotización: {ex.Message}");
                _cotizacionActual = null;
            }
        }

        private void CargarCotizacion()
        {
            try
            {
                _cotizacionActual = _cotizacionService.ObtenerPorId(_idCotizacion);
                
                if (_cotizacionActual == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error",
                        "alert('Cotización no encontrada.'); window.location = 'VerCotizaciones.aspx';", true);
                    return;
                }

                // Verificar permisos - solo el vendedor que la creó o admin puede editarla
                var usuarioActual = AuthHelper.GetCurrentUserId();
                var rolActual = AuthHelper.GetCurrentUserRole();
                
                if (usuarioActual != _cotizacionActual.IdUsuarioVendedor && 
                    !rolActual.Equals("Administrador", StringComparison.OrdinalIgnoreCase) &&
                    !rolActual.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error",
                        "alert('No tiene permisos para editar esta cotización.'); window.location = 'VerCotizaciones.aspx';", true);
                    return;
                }

                // Llenar campos
                txtFolio.Text = _cotizacionActual.Folio;
                ddlEstatus.SelectedValue = _cotizacionActual.Estatus;
                txtFechaEmision.Text = _cotizacionActual.FechaEmision.ToString("yyyy-MM-dd");
                txtFechaVencimiento.Text = _cotizacionActual.FechaVencimiento.ToString("yyyy-MM-dd");
                ddlSucursal.SelectedValue = _cotizacionActual.IdSucursal.ToString();
                txtNota.Text = _cotizacionActual.Nota ?? "";
                txtComentario.Text = _cotizacionActual.Comentario ?? "";

                // Información del vendedor
                var vendedor = _usuarioService.ObtenerPorId(_cotizacionActual.IdUsuarioVendedor);
                txtVendedor.Text = vendedor != null ? $"{vendedor.Nombre} {vendedor.ApPaterno}" : "No disponible";

                // Estado actual
                ActualizarEstadoActual();

                // Información de versión
                lblVersion.Text = $"{_cotizacionActual.Version} / {_cotizacionActual.UltimaVersion}";
                lblFechaActualizacion.Text = _cotizacionActual.FechaGuardado.ToString("dd/MM/yyyy HH:mm");

                // Cargar servicios
                CargarServicios();
                
                // Calcular totales
                CalcularTotales();

                // Desconectar la entidad del contexto y guardar en sesión para uso posterior
                _sharedContext.Entry(_cotizacionActual).State = System.Data.Entity.EntityState.Detached;
                Session["CotizacionEditar"] = _cotizacionActual;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al cargar cotización: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void CargarDropDownSucursales()
        {
            try
            {
                var sucursales = _sucursalService.ObtenerTodos().ToList();
                ddlSucursal.Items.Clear();
                ddlSucursal.Items.Add(new ListItem("Seleccione una sucursal", ""));
                
                foreach (var sucursal in sucursales)
                {
                    ddlSucursal.Items.Add(new ListItem($"{sucursal.NomComercial} ({sucursal.Empresa?.Nombre})", sucursal.IdSucursal.ToString()));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cargando dropdown sucursales: {ex.Message}");
            }
        }

        private void CargarServicios()
        {
            try
            {
                var servicios = _cotizacionServicioService.ObtenerPorCotizacion(_idCotizacion).ToList();
                gvServicios.DataSource = servicios;
                gvServicios.DataBind();
                
                lblTotalServicios.Text = servicios.Count.ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al cargar servicios: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void CalcularTotales()
        {
            try
            {
                if (_cotizacionActual != null)
                {
                    lblSubtotal.Text = _cotizacionActual.Subtotal.ToString("C");
                    lblIva.Text = _cotizacionActual.Iva.ToString("C");
                    lblTotal.Text = _cotizacionActual.Total.ToString("C");
                }
                else
                {
                    lblSubtotal.Text = "$0.00";
                    lblIva.Text = "$0.00";
                    lblTotal.Text = "$0.00";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error calculando totales: {ex.Message}");
                lblSubtotal.Text = "$0.00";
                lblIva.Text = "$0.00";
                lblTotal.Text = "$0.00";
            }
        }

        private void ActualizarEstadoActual()
        {
            if (_cotizacionActual == null)
            {
                lblEstadoActual.Text = "Error";
                lblEstadoActual.CssClass = "badge fs-6 p-3 bg-danger";
                return;
            }

            string estadoTexto = "";
            string badgeClass = "";

            switch (_cotizacionActual.Estatus)
            {
                case "P":
                    estadoTexto = "Pendiente";
                    badgeClass = "bg-warning text-dark";
                    break;
                case "A":
                    estadoTexto = "Aceptada";
                    badgeClass = "bg-success";
                    break;
                case "C":
                    estadoTexto = "Cancelada";
                    badgeClass = "bg-danger";
                    break;
                default:
                    estadoTexto = "Desconocido";
                    badgeClass = "bg-secondary";
                    break;
            }

            lblEstadoActual.Text = estadoTexto;
            lblEstadoActual.CssClass = $"badge fs-6 p-3 {badgeClass}";
        }

        protected void ddlEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Asegurar que la cotización esté cargada
            if (_cotizacionActual == null)
            {
                RecuperarCotizacion();
            }

            // Actualizar la visualización del estado
            if (_cotizacionActual != null)
            {
                _cotizacionActual.Estatus = ddlEstatus.SelectedValue;
                ActualizarEstadoActual();
                
                // Desconectar la entidad y actualizar en sesión
                _sharedContext.Entry(_cotizacionActual).State = System.Data.Entity.EntityState.Detached;
                Session["CotizacionEditar"] = _cotizacionActual;
                
                string script = $"actualizarEstadoBadge('{ddlEstatus.SelectedValue}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "actualizarBadge", script, true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    "alert('Error: No se pudo cargar la cotización.');", true);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Asegurar que la cotización esté cargada
                if (_cotizacionActual == null)
                {
                    RecuperarCotizacion();
                }

                if (_cotizacionActual == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error",
                        "alert('Error: Cotización no cargada.');", true);
                    return;
                }

                // Actualizar datos
                _cotizacionActual.Estatus = ddlEstatus.SelectedValue;
                _cotizacionActual.FechaEmision = Convert.ToDateTime(txtFechaEmision.Text);
                _cotizacionActual.FechaVencimiento = Convert.ToDateTime(txtFechaVencimiento.Text);
                _cotizacionActual.IdSucursal = Convert.ToInt32(ddlSucursal.SelectedValue);
                _cotizacionActual.Nota = txtNota.Text?.Trim();
                _cotizacionActual.Comentario = txtComentario.Text?.Trim();
                _cotizacionActual.FechaGuardado = DateTime.Now;
                _cotizacionActual.Version++;

                // Reconectar la entidad al contexto si está desconectada
                if (_sharedContext.Entry(_cotizacionActual).State == System.Data.Entity.EntityState.Detached)
                {
                    _sharedContext.Cotizaciones.Attach(_cotizacionActual);
                    _sharedContext.Entry(_cotizacionActual).State = System.Data.Entity.EntityState.Modified;
                }

                // Guardar cambios
                _cotizacionService.Actualizar(_cotizacionActual);

                // Desconectar nuevamente y actualizar en sesión
                _sharedContext.Entry(_cotizacionActual).State = System.Data.Entity.EntityState.Detached;
                Session["CotizacionEditar"] = _cotizacionActual;

                // Actualizar visualización
                lblFechaActualizacion.Text = _cotizacionActual.FechaGuardado.ToString("dd/MM/yyyy HH:mm");
                lblVersion.Text = $"{_cotizacionActual.Version} / {_cotizacionActual.UltimaVersion}";

                ScriptManager.RegisterStartupScript(this, GetType(), "success",
                    "alert('Cotización actualizada correctamente.'); actualizarFechaModificacion();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al guardar: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        protected void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                string nuevoEstado = btn.CommandArgument;
                string estadoTexto = GetEstadoTexto(nuevoEstado);

                // Asegurar que la cotización esté cargada
                if (_cotizacionActual == null)
                {
                    RecuperarCotizacion();
                }

                if (_cotizacionActual == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error",
                        "alert('Error: Cotización no cargada.');", true);
                    return;
                }

                // Validar cambio de estado
                if (_cotizacionActual.Estatus == nuevoEstado)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "info",
                        $"alert('La cotización ya tiene el estado \"{estadoTexto}\".');", true);
                    return;
                }

                // Confirmar cambio
                string confirmScript = $@"
                    if (confirmarCambioEstado('{nuevoEstado}', '{estadoTexto}')) {{
                        document.getElementById('{ddlEstatus.ClientID}').value = '{nuevoEstado}';
                        {ClientScript.GetPostBackEventReference(btnGuardar, "")}
                    }}
                ";
                
                ScriptManager.RegisterStartupScript(this, GetType(), "confirmarCambio", confirmScript, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al cambiar estado: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        // Métodos helper
        private string GetEstadoTexto(string estado)
        {
            switch (estado)
            {
                case "P": return "Pendiente";
                case "A": return "Aceptada";
                case "C": return "Cancelada";
                default: return "Desconocido";
            }
        }

        protected string GetHistorialBadgeClass(object estadoAnterior)
        {
            if (estadoAnterior == null) return "secondary";
            
            string estado = estadoAnterior.ToString();
            switch (estado)
            {
                case "P": return "warning";
                case "A": return "success";
                case "C": return "danger";
                default: return "secondary";
            }
        }

        private string EscapeForJavaScript(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            return text.Replace("'", "\\'")
                      .Replace("\"", "\\\"")
                      .Replace("\n", "\\n")
                      .Replace("\r", "\\r");
        }

        // Implementación de IDisposable para liberar el contexto de Entity Framework
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _sharedContext?.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~EditarCotizacion()
        {
            Dispose(false);
        }
    }
}