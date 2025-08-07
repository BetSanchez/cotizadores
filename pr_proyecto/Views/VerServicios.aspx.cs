using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using pr_proyecto.Helpers;
using pr_proyecto.Models;
using pr_proyecto.Repository;
using pr_proyecto.Services;

namespace pr_proyecto.Views
{
    public partial class VerServicios : System.Web.UI.Page
    {
        private readonly IServicioService _servicioService;
        private List<Servicio> _serviciosFiltrados;

        public VerServicios()
        {
            var servicioRepository = new ServicioRepository(new Data.AppDbContext());
            _servicioService = new ServicioService(servicioRepository);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar autenticación
            AuthHelper.RequireAuthentication();

            if (!IsPostBack)
            {
                CargarServicios();
            }
        }

        private void CargarServicios()
        {
            try
            {
                var todosLosServicios = _servicioService.ObtenerTodos().ToList();
                _serviciosFiltrados = AplicarFiltros(todosLosServicios);
                
                gvServicios.DataSource = _serviciosFiltrados;
                gvServicios.DataBind();

                // Actualizar contador
                lblTotalServicios.Text = _serviciosFiltrados.Count.ToString();

                // Enviar datos a JavaScript
                EnviarDatosServiciosAJavaScript();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al cargar servicios: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private List<Servicio> AplicarFiltros(List<Servicio> servicios)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                return servicios;

            string termino = txtBuscar.Text.Trim().ToLower();
            return servicios.Where(s => 
                (s.Nombre?.ToLower().Contains(termino) ?? false) ||
                (s.Descripcion?.ToLower().Contains(termino) ?? false) ||
                (s.Terminos?.ToLower().Contains(termino) ?? false) ||
                (s.Incluye?.ToLower().Contains(termino) ?? false) ||
                (s.Condiciones?.ToLower().Contains(termino) ?? false)
            ).ToList();
        }

        private void EnviarDatosServiciosAJavaScript()
        {
            try
            {
                if (_serviciosFiltrados != null && _serviciosFiltrados.Count > 0)
                {
                    var serviciosJs = _serviciosFiltrados.Select(s => new
                    {
                        IdServicio = s.IdServicio,
                        Nombre = s.Nombre ?? "",
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarServicios();
        }

        protected void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            CargarServicios();
        }

        protected void gvServicios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvServicios.PageIndex = e.NewPageIndex;
            CargarServicios();
        }

        protected void btnCargarDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hdnIdServicio.Value))
                    return;

                int idServicio = Convert.ToInt32(hdnIdServicio.Value);
                var servicio = _servicioService.ObtenerPorId(idServicio);
                
                if (servicio != null)
                {
                    // Llenar modal desde servidor
                    string script = $@"
                        document.getElementById('detalleNombre').textContent = '{EscapeForJavaScript(servicio.Nombre)}';
                        document.getElementById('detalleDescripcion').textContent = '{EscapeForJavaScript(servicio.Descripcion ?? "No especificado")}';
                        document.getElementById('detalleTerminos').textContent = '{EscapeForJavaScript(servicio.Terminos ?? "No especificado")}';
                        document.getElementById('detalleIncluye').textContent = '{EscapeForJavaScript(servicio.Incluye ?? "No especificado")}';
                        document.getElementById('detalleCondiciones').textContent = '{EscapeForJavaScript(servicio.Condiciones ?? "No especificado")}';
                        
                        var modal = new bootstrap.Modal(document.getElementById('modalDetalleServicio'));
                        modal.show();
                    ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showDetailModal", script, true);
                }

                // Limpiar campo oculto
                hdnIdServicio.Value = "";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al cargar detalle: {EscapeForJavaScript(ex.Message)}');", true);
            }
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
    }
}