using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pr_proyecto.Models;
using pr_proyecto.Repository;
using pr_proyecto.Services;
using pr_proyecto.Helpers;
using System.Web.Script.Serialization;

namespace pr_proyecto.Views
{
    public partial class Crear_cotizacion : System.Web.UI.Page
    {
        private readonly ICotizacionService _cotizacionService;
        private readonly IServicioService _servicioService;
        private readonly ISucursalService _sucursalService;
        private List<CotizacionServicio> _serviciosSeleccionados;

        public Crear_cotizacion()
        {
            var cotizacionRepository = new CotizacionRepository(new Data.AppDbContext());
            var cotizacionServicioRepository = new CotizacionServicioRepository(new Data.AppDbContext());
            var servicioRepository = new ServicioRepository(new Data.AppDbContext());
            var sucursalRepository = new SucursalRepository(new Data.AppDbContext());

            _cotizacionService = new CotizacionService(cotizacionRepository, cotizacionServicioRepository);
            _servicioService = new ServicioService(servicioRepository);
            _sucursalService = new SucursalService(sucursalRepository);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar autenticación
            AuthHelper.RequireAuthentication();

            // Inicializar la lista de servicios seleccionados
            _serviciosSeleccionados = Session["ServiciosSeleccionados"] as List<CotizacionServicio>;
            if (_serviciosSeleccionados == null)
            {
                _serviciosSeleccionados = new List<CotizacionServicio>();
                Session["ServiciosSeleccionados"] = _serviciosSeleccionados;
            }

            if (!IsPostBack)
            {
                // Cargar sucursales
                CargarSucursales();

                // Cargar servicios
                CargarServicios();

                // Generar folio automático
                GenerarFolio();

                // Establecer fecha de emisión como hoy
                txtFechaEmision.Text = DateTime.Today.ToString("yyyy-MM-dd");

                // Establecer fecha de vencimiento como 15 días después
                txtFechaVencimiento.Text = DateTime.Today.AddDays(15).ToString("yyyy-MM-dd");
            }

            ActualizarGridServicios();
        }

        private void CargarSucursales()
        {
            var sucursales = _sucursalService.ObtenerTodas();
            ddlSucursal.DataSource = sucursales;
            ddlSucursal.DataTextField = "NomComercial";
            ddlSucursal.DataValueField = "IdSucursal";
            ddlSucursal.DataBind();
        }

        private void CargarServicios()
        {
            var servicios = _servicioService.ObtenerTodos();
            ddlServicios.DataSource = servicios;
            ddlServicios.DataTextField = "Nombre";
            ddlServicios.DataValueField = "IdServicio";
            ddlServicios.DataBind();
            ddlServicios.Items.Insert(0, new ListItem("-- Seleccione un servicio --", ""));
        }

        private void GenerarFolio()
        {
            // Obtener el último folio y generar el siguiente
            var ultimaCotizacion = _cotizacionService.ObtenerUltimaCotizacion();
            int numeroFolio = 1;

            if (ultimaCotizacion != null)
            {
                string ultimoFolio = ultimaCotizacion.Folio;
                if (ultimoFolio.StartsWith("COT-"))
                {
                    int.TryParse(ultimoFolio.Substring(4), out numeroFolio);
                    numeroFolio++;
                }
            }

            txtFolio.Text = $"COT-{numeroFolio:D4}";
        }

        // Método auxiliar para escapar texto para JavaScript
        private string EscapeForJavaScript(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            return text.Replace("'", "\\'")
                      .Replace("\"", "\\\"")
                      .Replace("\n", "\\n")
                      .Replace("\r", "\\r");
        }

        // Método para generar los botones de acción
        protected string GetBotonesAccion(int indice)
        {
            return $@"
                <button type='button' class='btn btn-primary btn-sm me-1' onclick='editarServicio({indice})'>
                    Editar
                </button>
                <button type='button' class='btn btn-danger btn-sm' onclick='eliminarServicio({indice})'>
                    Eliminar
                </button>
            ";
        }

        protected void ddlServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlServicios.SelectedValue))
            {
                // Limpiar información del servicio si no hay selección
                LimpiarInformacionServicio();
                return;
            }

            int idServicio = Convert.ToInt32(ddlServicios.SelectedValue);
            var servicio = _servicioService.ObtenerPorId(idServicio);

            if (servicio != null)
            {
                // Guardar el servicio en la sesión para usarlo en el modal
                Session["ServicioSeleccionado"] = servicio;

                // Actualizar información del servicio en la página (sin abrir modal automáticamente)
                string script = $@"
                    document.getElementById('lblNombreServicio').textContent = '{EscapeForJavaScript(servicio.Nombre)}';
                    document.getElementById('lblDescripcionServicio').textContent = '{EscapeForJavaScript(servicio.Descripcion ?? "")}';
                    document.getElementById('lblTerminosServicio').textContent = '{EscapeForJavaScript(servicio.Terminos ?? "")}';
                    document.getElementById('lblIncluyeServicio').textContent = '{EscapeForJavaScript(servicio.Incluye ?? "")}';
                    document.getElementById('lblCondicionesServicio').textContent = '{EscapeForJavaScript(servicio.Condiciones ?? "")}';
                    
                    // Pre-llenar campos con información del servicio
                    document.getElementById('{txtDescripcionServicio.ClientID}').value = '{EscapeForJavaScript(servicio.Descripcion ?? "")}';
                    document.getElementById('{txtTerminos.ClientID}').value = '{EscapeForJavaScript(servicio.Terminos ?? "")}';
                    document.getElementById('{txtIncluye.ClientID}').value = '{EscapeForJavaScript(servicio.Incluye ?? "")}';
                    document.getElementById('{txtCondiciones.ClientID}').value = '{EscapeForJavaScript(servicio.Condiciones ?? "")}';
                ";

                ScriptManager.RegisterStartupScript(this, GetType(), "actualizarInfoServicio", script, true);
            }
        }

        private void LimpiarInformacionServicio()
        {
            string script = @"
                document.getElementById('lblNombreServicio').textContent = '';
                document.getElementById('lblDescripcionServicio').textContent = '';
                document.getElementById('lblTerminosServicio').textContent = '';
                document.getElementById('lblIncluyeServicio').textContent = '';
                document.getElementById('lblCondicionesServicio').textContent = '';
            ";
            ScriptManager.RegisterStartupScript(this, GetType(), "limpiarInfoServicio", script, true);
        }

        protected void btnAgregarServicio_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que la lista de servicios esté inicializada
                if (_serviciosSeleccionados == null)
                {
                    _serviciosSeleccionados = new List<CotizacionServicio>();
                    Session["ServiciosSeleccionados"] = _serviciosSeleccionados;
                }

                // Obtener el servicio seleccionado de la sesión
                var servicio = Session["ServicioSeleccionado"] as Servicio;
                if (servicio == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                        "alert('Debe seleccionar un servicio primero.');", true);
                    return;
                }

                // Validar campos requeridos
                if (string.IsNullOrWhiteSpace(txtCantidad.Text) || string.IsNullOrWhiteSpace(txtValorUnitario.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                        "alert('La cantidad y valor unitario son requeridos.');", true);
                    return;
                }

                if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                        "alert('La cantidad debe ser un número mayor a 0.');", true);
                    return;
                }

                if (!double.TryParse(txtValorUnitario.Text, out double valorUnitario) || valorUnitario < 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                        "alert('El valor unitario debe ser un número mayor o igual a 0.');", true);
                    return;
                }

                // Obtener valores de los campos personalizados
                string descripcionPersonalizada = txtDescripcionServicio.Text.Trim();
                string terminosPersonalizados = txtTerminos.Text.Trim();
                string incluyePersonalizado = txtIncluye.Text.Trim();
                string condicionesPersonalizadas = txtCondiciones.Text.Trim();

                // Usar valores personalizados si están llenos, sino usar los del servicio base
                var cotizacionServicio = new CotizacionServicio
                {
                    IdServicio = servicio.IdServicio,
                    // NO asignar Servicio para evitar problemas de tracking
                    Cantidad = cantidad,
                    ValorUnitario = valorUnitario,
                    Subtotal = cantidad * valorUnitario,
                    Descripcion = !string.IsNullOrEmpty(descripcionPersonalizada) ? descripcionPersonalizada : servicio.Descripcion,
                    Terminos = !string.IsNullOrEmpty(terminosPersonalizados) ? terminosPersonalizados : servicio.Terminos,
                    Incluye = !string.IsNullOrEmpty(incluyePersonalizado) ? incluyePersonalizado : servicio.Incluye,
                    Condiciones = !string.IsNullOrEmpty(condicionesPersonalizadas) ? condicionesPersonalizadas : servicio.Condiciones
                };

                _serviciosSeleccionados.Add(cotizacionServicio);
                Session["ServiciosSeleccionados"] = _serviciosSeleccionados;

                LimpiarFormularioServicio();
                ActualizarGridServicios();
                CalcularTotales();

                // Cerrar modal
                string closeModalScript = @"
                    setTimeout(function() {
                        var modal = document.getElementById('modalServicio');
                        if (modal && typeof bootstrap !== 'undefined') {
                            var bootstrapModal = bootstrap.Modal.getInstance(modal);
                            if (bootstrapModal) {
                                bootstrapModal.hide();
                            } else {
                                // Si no hay instancia, intentar crear una nueva y cerrarla
                                var newModal = new bootstrap.Modal(modal);
                                newModal.hide();
                            }
                        } else if (modal) {
                            // Fallback: usar jQuery si Bootstrap no está disponible
                            if (typeof $ !== 'undefined') {
                                $(modal).modal('hide');
                            } else {
                                // Último recurso: remover clases manualmente
                                modal.classList.remove('show');
                                modal.style.display = 'none';
                                document.body.classList.remove('modal-open');
                                var backdrop = document.querySelector('.modal-backdrop');
                                if (backdrop) backdrop.remove();
                            }
                        }
                    }, 100);
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "cerrarModal", closeModalScript, true);

                ScriptManager.RegisterStartupScript(this, GetType(), "alertaExito",
                    $"alert('Servicio \"{servicio.Nombre}\" agregado correctamente. Total: {_serviciosSeleccionados.Count} servicios.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                    $"alert('Error al agregar el servicio: {EscapeForJavaScript(ex.Message)}\\n\\nStackTrace: {EscapeForJavaScript(ex.StackTrace)}');", true);
            }
        }

        protected void btnAccionOculto_Click(object sender, EventArgs e)
        {
            try
            {
                // Asegurar que la lista esté inicializada
                if (_serviciosSeleccionados == null)
                {
                    _serviciosSeleccionados = Session["ServiciosSeleccionados"] as List<CotizacionServicio>;
                    if (_serviciosSeleccionados == null)
                    {
                        _serviciosSeleccionados = new List<CotizacionServicio>();
                        Session["ServiciosSeleccionados"] = _serviciosSeleccionados;
                    }
                }

                string accion = hdnAccion.Value;
                int index = Convert.ToInt32(hdnIndiceServicio.Value);

                // Validar que el índice sea válido
                if (index < 0 || index >= _serviciosSeleccionados.Count)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                        $"alert('Error: Índice de servicio no válido ({index}). Total de servicios: {_serviciosSeleccionados.Count}');", true);
                    return;
                }

                if (accion == "eliminar")
                {
                    var servicioEliminado = _serviciosSeleccionados[index];
                    _serviciosSeleccionados.RemoveAt(index);
                    Session["ServiciosSeleccionados"] = _serviciosSeleccionados;
                    ActualizarGridServicios();
                    CalcularTotales();
                    EnviarDatosServiciosAJavaScript();

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaExito",
                        $"alert('Servicio \"{EscapeForJavaScript(servicioEliminado.Servicio?.Nombre ?? "N/A")}\" eliminado correctamente.');", true);
                }

                // Limpiar campos ocultos
                hdnAccion.Value = "";
                hdnIndiceServicio.Value = "";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                    $"alert('Error al procesar la acción: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        protected void btnActualizarServicio_Click(object sender, EventArgs e)
        {
            try
            {
                // Asegurar que la lista esté inicializada
                if (_serviciosSeleccionados == null)
                {
                    _serviciosSeleccionados = Session["ServiciosSeleccionados"] as List<CotizacionServicio>;
                    if (_serviciosSeleccionados == null)
                    {
                        _serviciosSeleccionados = new List<CotizacionServicio>();
                        Session["ServiciosSeleccionados"] = _serviciosSeleccionados;
                    }
                }

                if (string.IsNullOrEmpty(hdnIndiceServicio.Value))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                        "alert('Error: No se encontró el índice del servicio a editar.');", true);
                    return;
                }

                int indiceEditando = Convert.ToInt32(hdnIndiceServicio.Value);

                if (indiceEditando < 0 || indiceEditando >= _serviciosSeleccionados.Count)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                        $"alert('Error: Índice de servicio no válido ({indiceEditando}). Total de servicios: {_serviciosSeleccionados.Count}');", true);
                    return;
                }

                var servicioEditando = _serviciosSeleccionados[indiceEditando];

                // Validar datos
                if (string.IsNullOrWhiteSpace(txtCantidadEditar.Text) || string.IsNullOrWhiteSpace(txtValorUnitarioEditar.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                        "alert('La cantidad y valor unitario son requeridos.');", true);
                    return;
                }

                if (!int.TryParse(txtCantidadEditar.Text, out int cantidad) || cantidad <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                        "alert('La cantidad debe ser un número mayor a 0.');", true);
                    return;
                }

                if (!double.TryParse(txtValorUnitarioEditar.Text, out double valorUnitario) || valorUnitario < 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                        "alert('El valor unitario debe ser un número mayor o igual a 0.');", true);
                    return;
                }

                // Actualizar el servicio
                servicioEditando.Cantidad = cantidad;
                servicioEditando.ValorUnitario = valorUnitario;
                servicioEditando.Subtotal = cantidad * valorUnitario;
                servicioEditando.Descripcion = txtDescripcionServicioEditar.Text.Trim();
                servicioEditando.Terminos = txtTerminosEditar.Text.Trim();
                servicioEditando.Incluye = txtIncluyeEditar.Text.Trim();
                servicioEditando.Condiciones = txtCondicionesEditar.Text.Trim();

                // Actualizar en la lista
                _serviciosSeleccionados[indiceEditando] = servicioEditando;
                Session["ServiciosSeleccionados"] = _serviciosSeleccionados;

                // Limpiar campo oculto
                hdnIndiceServicio.Value = "";

                // Limpiar formulario
                LimpiarFormularioEditar();

                // Actualizar grid y totales
                ActualizarGridServicios();
                CalcularTotales();
                EnviarDatosServiciosAJavaScript();

                // Cerrar modal
                string closeModalScript = @"
                    setTimeout(function() {
                        var modal = document.getElementById('modalEditarServicio');
                        if (modal && typeof bootstrap !== 'undefined') {
                            var bootstrapModal = bootstrap.Modal.getInstance(modal);
                            if (bootstrapModal) {
                                bootstrapModal.hide();
                            } else {
                                // Si no hay instancia, intentar crear una nueva y cerrarla
                                var newModal = new bootstrap.Modal(modal);
                                newModal.hide();
                            }
                        } else if (modal) {
                            // Fallback: usar jQuery si Bootstrap no está disponible
                            if (typeof $ !== 'undefined') {
                                $(modal).modal('hide');
                            } else {
                                // Último recurso: remover clases manualmente
                                modal.classList.remove('show');
                                modal.style.display = 'none';
                                document.body.classList.remove('modal-open');
                                var backdrop = document.querySelector('.modal-backdrop');
                                if (backdrop) backdrop.remove();
                            }
                        }
                    }, 100);
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "cerrarModalEditar", closeModalScript, true);

                ScriptManager.RegisterStartupScript(this, GetType(), "alertaExito",
                    $"alert('Servicio \"{EscapeForJavaScript(servicioEditando.Servicio?.Nombre ?? "N/A")}\" actualizado correctamente.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                    $"alert('Error al actualizar el servicio: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void ActualizarGridServicios()
        {
            try
            {
                if (_serviciosSeleccionados != null)
                {
                    gvServicios.DataSource = _serviciosSeleccionados;
                    gvServicios.DataBind();
                }
                else
                {
                    gvServicios.DataSource = new List<CotizacionServicio>();
                    gvServicios.DataBind();
                }
                
                // Enviar datos a JavaScript después de actualizar el grid
                EnviarDatosServiciosAJavaScript();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                    $"alert('Error al actualizar el grid de servicios: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void EnviarDatosServiciosAJavaScript()
        {
            try
            {
                if (_serviciosSeleccionados != null && _serviciosSeleccionados.Count > 0)
                {
                    var serviciosJs = _serviciosSeleccionados.Select((s, index) => new
                    {
                        Indice = index,
                        NombreServicio = s.Servicio?.Nombre ?? "",
                        DescripcionBase = s.Servicio?.Descripcion ?? "",
                        TerminosBase = s.Servicio?.Terminos ?? "",
                        IncluyeBase = s.Servicio?.Incluye ?? "",
                        CondicionesBase = s.Servicio?.Condiciones ?? "",
                        Cantidad = s.Cantidad,
                        ValorUnitario = s.ValorUnitario,
                        Descripcion = s.Descripcion ?? "",
                        Terminos = s.Terminos ?? "",
                        Incluye = s.Incluye ?? "",
                        Condiciones = s.Condiciones ?? ""
                    }).ToList();

                    var serializer = new JavaScriptSerializer();
                    string jsonServicios = serializer.Serialize(serviciosJs);
                    string script = $"actualizarServiciosData({jsonServicios});";
                    ScriptManager.RegisterStartupScript(this, GetType(), "actualizarServicios", script, true);
                }
                else
                {
                    string script = "actualizarServiciosData([]);";
                    ScriptManager.RegisterStartupScript(this, GetType(), "actualizarServicios", script, true);
                }
            }
            catch (Exception ex)
            {
                // Error silencioso para no interrumpir el flujo
                System.Diagnostics.Debug.WriteLine($"Error al enviar datos a JavaScript: {ex.Message}");
            }
        }

        private void CalcularTotales()
        {
            try
            {
                if (_serviciosSeleccionados != null && _serviciosSeleccionados.Count > 0)
                {
                    double subtotal = _serviciosSeleccionados.Sum(s => s.Subtotal);
                    double iva = subtotal * 0.16; // 16% de IVA
                    double total = subtotal + iva;

                    lblSubtotal.Text = subtotal.ToString("C");
                    lblIVA.Text = iva.ToString("C");
                    lblTotal.Text = total.ToString("C");
                }
                else
                {
                    lblSubtotal.Text = "$0.00";
                    lblIVA.Text = "$0.00";
                    lblTotal.Text = "$0.00";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                    $"alert('Error al calcular totales: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void LimpiarFormularioServicio()
        {
            txtCantidad.Text = string.Empty;
            txtValorUnitario.Text = string.Empty;
            txtDescripcionServicio.Text = string.Empty;
            txtTerminos.Text = string.Empty;
            txtIncluye.Text = string.Empty;
            txtCondiciones.Text = string.Empty;
            ddlServicios.SelectedIndex = 0;

            // Limpiar información del servicio en la sesión
            Session.Remove("ServicioSeleccionado");

            // Limpiar información visual del servicio
            LimpiarInformacionServicio();
        }

        private void LimpiarFormularioEditar()
        {
            txtCantidadEditar.Text = string.Empty;
            txtValorUnitarioEditar.Text = string.Empty;
            txtDescripcionServicioEditar.Text = string.Empty;
            txtTerminosEditar.Text = string.Empty;
            txtIncluyeEditar.Text = string.Empty;
            txtCondicionesEditar.Text = string.Empty;
        }

        public void btnVistaPrevia_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serviciosSeleccionados == null || _serviciosSeleccionados.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                        "alert('Debe agregar al menos un servicio a la cotización para generar la vista previa.');", true);
                    return;
                }

                // Generar HTML de servicios para la vista previa
                string serviciosHtml = GenerarHtmlServicios();

                string script = $@"
                    // Llenar información general
                    document.getElementById('previewFolio').textContent = '{EscapeForJavaScript(txtFolio.Text)}';
                    document.getElementById('previewFechaEmision').textContent = '{EscapeForJavaScript(txtFechaEmision.Text)}';
                    document.getElementById('previewFechaVencimiento').textContent = '{EscapeForJavaScript(txtFechaVencimiento.Text)}';
                    document.getElementById('previewSucursal').textContent = '{EscapeForJavaScript(ddlSucursal.SelectedItem?.Text ?? "")}';
                    document.getElementById('previewNota').textContent = '{EscapeForJavaScript(txtNota.Text ?? "Sin nota")}';
                    document.getElementById('previewComentario').textContent = '{EscapeForJavaScript(txtComentario.Text ?? "Sin comentario")}';
                    document.getElementById('previewSubtotal').textContent = '{EscapeForJavaScript(lblSubtotal.Text)}';
                    document.getElementById('previewIVA').textContent = '{EscapeForJavaScript(lblIVA.Text)}';
                    document.getElementById('previewTotal').textContent = '{EscapeForJavaScript(lblTotal.Text)}';
                    
                    // Llenar servicios
                    document.getElementById('previewServicios').innerHTML = `{serviciosHtml}`;
                    
                    // Mostrar modal usando Bootstrap
                    var modal = document.getElementById('modalVistaPrevia');
                    if (modal) {{
                        var bootstrapModal = new bootstrap.Modal(modal);
                        bootstrapModal.show();
                    }}
                ";

                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarVistaPrevia", script, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                    $"alert('Error al generar la vista previa: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private string GenerarHtmlServicios()
        {
            var html = new System.Text.StringBuilder();
            html.Append("<table class='table table-striped table-sm'>");
            html.Append("<thead><tr><th>Servicio</th><th>Descripción</th><th>Cantidad</th><th>Valor Unitario</th><th>Subtotal</th><th>Términos</th><th>Incluye</th><th>Condiciones</th></tr></thead><tbody>");

            foreach (var servicio in _serviciosSeleccionados)
            {
                html.Append("<tr>");
                html.Append($"<td><strong>{HttpUtility.HtmlEncode(servicio.Servicio?.Nombre ?? "N/A")}</strong></td>");
                html.Append($"<td>{HttpUtility.HtmlEncode(servicio.Descripcion ?? "N/A")}</td>");
                html.Append($"<td>{servicio.Cantidad}</td>");
                html.Append($"<td>{servicio.ValorUnitario:C}</td>");
                html.Append($"<td>{servicio.Subtotal:C}</td>");
                html.Append($"<td>{HttpUtility.HtmlEncode(servicio.Terminos ?? "N/A")}</td>");
                html.Append($"<td>{HttpUtility.HtmlEncode(servicio.Incluye ?? "N/A")}</td>");
                html.Append($"<td>{HttpUtility.HtmlEncode(servicio.Condiciones ?? "N/A")}</td>");
                html.Append("</tr>");
            }

            html.Append("</tbody></table>");
            return html.ToString();
        }

        protected void btnDebug_Click(object sender, EventArgs e)
        {
            try
            {
                var info = $"Información de depuración:\\n";
                info += $"Servicios en sesión: {_serviciosSeleccionados?.Count ?? 0}\\n";
                info += $"EditIndex del GridView: {gvServicios.EditIndex}\\n";
                info += $"Total de filas en GridView: {gvServicios.Rows.Count}\\n";
                info += $"Session ID: {Session.SessionID}\\n";
                info += $"Servicio seleccionado: {ddlServicios.SelectedValue}\\n";
                info += $"Cantidad: {txtCantidad.Text}\\n";
                info += $"Valor Unitario: {txtValorUnitario.Text}\\n";

                // Verificar si hay servicios en la base de datos
                var serviciosDisponibles = _servicioService.ObtenerTodos();
                int countServicios = serviciosDisponibles?.Count() ?? 0;
                info += $"\\nServicios disponibles en BD: {countServicios}\\n";

                if (_serviciosSeleccionados != null && _serviciosSeleccionados.Count > 0)
                {
                    info += $"\\nServicios en la lista:\\n";
                    for (int i = 0; i < _serviciosSeleccionados.Count; i++)
                    {
                        var servicio = _serviciosSeleccionados[i];
                        info += $"{i}: {servicio.Servicio?.Nombre} - {servicio.Descripcion}\\n";
                    }
                }

                // Verificar el estado de la sesión
                var sessionServicios = Session["ServiciosSeleccionados"] as List<CotizacionServicio>;
                info += $"\\nServicios en Session: {sessionServicios?.Count ?? 0}\\n";

                ScriptManager.RegisterStartupScript(this, GetType(), "debugInfo",
                    $"alert('{info}');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                    $"alert('Error en debug: {EscapeForJavaScript(ex.Message)}\\n\\nStackTrace: {EscapeForJavaScript(ex.StackTrace)}');", true);
            }
        }

        protected void btnTestAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que la lista de servicios esté inicializada
                if (_serviciosSeleccionados == null)
                {
                    _serviciosSeleccionados = new List<CotizacionServicio>();
                    Session["ServiciosSeleccionados"] = _serviciosSeleccionados;
                }

                // Crear un servicio de prueba
                var serviciosDisponibles = _servicioService.ObtenerTodos();
                var servicioTest = serviciosDisponibles?.FirstOrDefault();
                if (servicioTest != null)
                {
                    var cotizacionServicio = new CotizacionServicio
                    {
                        IdServicio = servicioTest.IdServicio,
                        // NO asignar Servicio para evitar problemas de tracking
                        Cantidad = 1,
                        ValorUnitario = 100.0,
                        Subtotal = 100.0,
                        Descripcion = "Servicio de prueba",
                        Terminos = "Términos de prueba",
                        Incluye = "Incluye de prueba",
                        Condiciones = "Condiciones de prueba"
                    };

                    _serviciosSeleccionados.Add(cotizacionServicio);
                    Session["ServiciosSeleccionados"] = _serviciosSeleccionados;

                    ActualizarGridServicios();
                    CalcularTotales();

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaExito",
                        $"alert('Servicio de prueba agregado correctamente. Total de servicios: {_serviciosSeleccionados.Count}');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                        "alert('No hay servicios disponibles en la base de datos.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                    $"alert('Error en prueba: {EscapeForJavaScript(ex.Message)}\\n\\nStackTrace: {EscapeForJavaScript(ex.StackTrace)}');", true);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar autenticación
                if (!AuthHelper.IsAuthenticated())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                        "alert('Su sesión ha expirado. Por favor, inicie sesión nuevamente.'); window.location = '/Views/Login.aspx';", true);
                    return;
                }

                if (_serviciosSeleccionados == null || _serviciosSeleccionados.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                        "alert('Debe agregar al menos un servicio a la cotización.');", true);
                    return;
                }

                // Validar que se haya seleccionado una sucursal
                if (string.IsNullOrEmpty(ddlSucursal.SelectedValue) || ddlSucursal.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                        "alert('Debe seleccionar una sucursal.');", true);
                    return;
                }

                // Obtener el usuario autenticado
                var usuarioId = AuthHelper.GetCurrentUserId();
                if (!usuarioId.HasValue)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                        "alert('Error: No se pudo obtener la información del usuario.');", true);
                    return;
                }

                var cotizacion = new Cotizacion
                {
                    Folio = txtFolio.Text.Trim(),
                    FechaEmision = Convert.ToDateTime(txtFechaEmision.Text),
                    FechaVencimiento = Convert.ToDateTime(txtFechaVencimiento.Text),
                    IdUsuarioVendedor = usuarioId.Value,
                    Nota = txtNota.Text?.Trim(),
                    Subtotal = _serviciosSeleccionados.Sum(s => s.Subtotal),
                    Iva = _serviciosSeleccionados.Sum(s => s.Subtotal) * 0.16,
                    Total = _serviciosSeleccionados.Sum(s => s.Subtotal) * 1.16,
                    Estatus = "A", // Activa
                    Version = 1,
                    UltimaVersion = 1,
                    FechaGuardado = DateTime.Now,
                    Comentario = txtComentario.Text?.Trim(),
                    IdUsuario = usuarioId.Value,
                    IdSucursal = Convert.ToInt32(ddlSucursal.SelectedValue)
                };

                try
                {
                    _cotizacionService.CrearCotizacion(cotizacion, _serviciosSeleccionados);

                    // Mostrar mensaje de éxito
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaExito",
                        "alert('Cotización guardada exitosamente.'); window.location = 'Dashboard.aspx';", true);
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    var mensaje = string.Join("\\n", dbEx.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage));

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                        $"alert('Error de validación al guardar la cotización:\\n{EscapeForJavaScript(mensaje)}');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertaError",
                    $"alert('Error al guardar la cotización: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }
    }
}