using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using pr_proyecto.Helpers;
using pr_proyecto.Models;
using pr_proyecto.Repository;
using pr_proyecto.Services;

namespace pr_proyecto.Views
{
    public partial class GestionServicios : System.Web.UI.Page
    {
        private readonly IServicioService _servicioService;
        private List<Servicio> _serviciosFiltrados;

        public GestionServicios()
        {
            var servicioRepository = new ServicioRepository(new Data.AppDbContext());
            _servicioService = new ServicioService(servicioRepository);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar autenticación y rol de administrador
            AuthHelper.RequireAnyRole("Administrador", "Admin");

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
                (s.Descripcion?.ToLower().Contains(termino) ?? false)
            ).ToList();
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

        protected void btnAccionOculto_Click(object sender, EventArgs e)
        {
            try
            {
                string accion = hdnAccion.Value;
                
                switch (accion)
                {
                    case "eliminar":
                        EliminarServicio();
                        break;
                    case "cargar_editar":
                        CargarServicioParaEditar();
                        break;
                }

                // Limpiar campos ocultos
                hdnAccion.Value = "";
                hdnIdServicio.Value = "";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void EliminarServicio()
        {
            if (string.IsNullOrEmpty(hdnIdServicio.Value))
                return;

            int idServicio = Convert.ToInt32(hdnIdServicio.Value);
            var servicio = _servicioService.ObtenerPorId(idServicio);
            
            if (servicio != null)
            {
                _servicioService.Eliminar(idServicio);
                CargarServicios();
                
                ScriptManager.RegisterStartupScript(this, GetType(), "success",
                    $"alert('Servicio \"{EscapeForJavaScript(servicio.Nombre)}\" eliminado correctamente.');", true);
            }
        }

        private void CargarServicioParaEditar()
        {
            if (string.IsNullOrEmpty(hdnIdServicio.Value))
                return;

            int idServicio = Convert.ToInt32(hdnIdServicio.Value);
            var servicio = _servicioService.ObtenerPorId(idServicio);
            
            if (servicio != null)
            {
                // Llenar campos del modal
                txtNombreServicio.Text = servicio.Nombre;
                txtDescripcionServicio.Text = servicio.Descripcion ?? "";
                txtTerminosServicio.Text = servicio.Terminos ?? "";
                txtIncluyeServicio.Text = servicio.Incluye ?? "";
                txtCondicionesServicio.Text = servicio.Condiciones ?? "";
                
                // Configurar para edición
                hdnAccion.Value = "editar";
                
                // Mostrar modal
                string script = @"
                    document.querySelector('#modalNuevoServicioLabel').textContent = 'Editar Servicio';
                    var modal = new bootstrap.Modal(document.getElementById('modalNuevoServicio'));
                    modal.show();
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "showEditModal", script, true);
            }
        }

        protected void btnGuardarServicio_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreServicio.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error",
                        "alert('El nombre del servicio es requerido.');", true);
                    return;
                }

                bool esEdicion = hdnAccion.Value == "editar" && !string.IsNullOrEmpty(hdnIdServicio.Value);
                
                Servicio servicio;
                if (esEdicion)
                {
                    // Editar servicio existente
                    int idServicio = Convert.ToInt32(hdnIdServicio.Value);
                    servicio = _servicioService.ObtenerPorId(idServicio);
                    if (servicio == null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "error",
                            "alert('Servicio no encontrado.');", true);
                        return;
                    }
                }
                else
                {
                    // Crear nuevo servicio
                    servicio = new Servicio();
                }

                // Asignar valores
                servicio.Nombre = txtNombreServicio.Text.Trim();
                servicio.Descripcion = txtDescripcionServicio.Text.Trim();
                servicio.Terminos = txtTerminosServicio.Text.Trim();
                servicio.Incluye = txtIncluyeServicio.Text.Trim();
                servicio.Condiciones = txtCondicionesServicio.Text.Trim();

                if (esEdicion)
                {
                    _servicioService.Actualizar(servicio);
                }
                else
                {
                    _servicioService.Agregar(servicio);
                }

                // Limpiar formulario
                LimpiarFormulario();
                CargarServicios();

                // Cerrar modal y mostrar mensaje
                string action = esEdicion ? "actualizado" : "creado";
                string script = $@"
                    var modal = bootstrap.Modal.getInstance(document.getElementById('modalNuevoServicio'));
                    if (modal) modal.hide();
                    alert('Servicio {action} correctamente.');
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "success", script, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al guardar servicio: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void LimpiarFormulario()
        {
            txtNombreServicio.Text = "";
            txtDescripcionServicio.Text = "";
            txtTerminosServicio.Text = "";
            txtIncluyeServicio.Text = "";
            txtCondicionesServicio.Text = "";
            hdnAccion.Value = "";
            hdnIdServicio.Value = "";
        }

        // Método helper para generar botones de acción
        protected string GetBotonesAccion(object idServicio)
        {
            if (idServicio == null) return "";
            
            return $@"
                <button type='button' class='btn btn-primary btn-sm me-1' onclick='editarServicio({idServicio})'>
                    <i class='fas fa-edit'></i> Editar
                </button>
                <button type='button' class='btn btn-danger btn-sm' onclick='eliminarServicio({idServicio}, ""Servicio"")'>
                    <i class='fas fa-trash'></i> Eliminar
                </button>
            ";
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