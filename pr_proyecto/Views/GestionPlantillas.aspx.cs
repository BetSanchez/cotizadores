using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pr_proyecto.Models;
using pr_proyecto.Services;
using pr_proyecto.Helpers;

namespace pr_proyecto.Views
{
    public partial class GestionPlantillas : System.Web.UI.Page
    {
        private readonly IServicioService _servicioService;
        private readonly IUsuarioService _usuarioService;

        public GestionPlantillas()
        {
            var context = new Data.AppDbContext();
            _servicioService = new ServicioService(new Repository.ServicioRepository(context));
            _usuarioService = new UsuarioService(new Repository.UsuarioRepository(context));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthHelper.RequireAuthentication();

            if (!IsPostBack)
            {
                CargarPlantillas();
                CargarServiciosParaAsignar();
                CargarUsuariosEnFiltro();
                CalcularResumen();
            }
        }

        private void CargarPlantillas()
        {
            try
            {
                // Por ahora usamos datos mock ya que no tenemos el servicio de plantillas implementado
                var plantillas = GetPlantillasMock();
                var plantillasFiltradas = AplicarFiltrosPlantillas(plantillas);
                
                gvPlantillas.DataSource = plantillasFiltradas;
                gvPlantillas.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al cargar plantillas: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void CargarServiciosParaAsignar()
        {
            try
            {
                var servicios = _servicioService.ObtenerTodos().ToList();
                
                cblServicios.DataSource = servicios;
                cblServicios.DataTextField = "Nombre";
                cblServicios.DataValueField = "IdServicio";
                cblServicios.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al cargar servicios: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void CargarUsuariosEnFiltro()
        {
            try
            {
                var usuarios = _usuarioService.ObtenerTodos().ToList();
                
                ddlFiltroUsuario.Items.Clear();
                ddlFiltroUsuario.Items.Add(new ListItem("Todos los usuarios", ""));
                
                foreach (var usuario in usuarios)
                {
                    string texto = $"{usuario.Nombre} {usuario.ApPaterno}";
                    ddlFiltroUsuario.Items.Add(new ListItem(texto, usuario.IdUsuario.ToString()));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al cargar usuarios: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private List<dynamic> GetPlantillasMock()
        {
            // Datos mock para demostración
            var currentUser = GetCurrentUserInfo();
            return new List<dynamic>
            {
                new {
                    IdPlantilla = 1,
                    Nombre = "Servicios Básicos Oficina",
                    Descripcion = "Plantilla con servicios básicos para oficinas pequeñas",
                    FechaCreacion = DateTime.Now.AddDays(-15),
                    IdUsuario = currentUser?.IdUsuario ?? 1,
                    Usuario = new { Nombre = currentUser?.Nombre ?? "Admin", ApPaterno = currentUser?.ApPaterno ?? "Sistema" }
                },
                new {
                    IdPlantilla = 2,
                    Nombre = "Servicios Avanzados Empresa",
                    Descripcion = "Plantilla completa para empresas medianas",
                    FechaCreacion = DateTime.Now.AddDays(-30),
                    IdUsuario = currentUser?.IdUsuario ?? 1,
                    Usuario = new { Nombre = currentUser?.Nombre ?? "Admin", ApPaterno = currentUser?.ApPaterno ?? "Sistema" }
                }
            };
        }

        private List<dynamic> AplicarFiltrosPlantillas(List<dynamic> plantillas)
        {
            var filtradas = plantillas.ToList();

            // Filtro por texto
            if (!string.IsNullOrWhiteSpace(txtBuscarPlantilla.Text))
            {
                string termino = txtBuscarPlantilla.Text.Trim().ToLower();
                filtradas = filtradas.Where(p => 
                    ((string)p.Nombre).ToLower().Contains(termino) ||
                    ((string)p.Descripcion).ToLower().Contains(termino)
                ).ToList();
            }

            // Filtro por usuario
            if (!string.IsNullOrEmpty(ddlFiltroUsuario.SelectedValue))
            {
                int idUsuario = Convert.ToInt32(ddlFiltroUsuario.SelectedValue);
                filtradas = filtradas.Where(p => (int)p.IdUsuario == idUsuario).ToList();
            }

            return filtradas;
        }

        private void CalcularResumen()
        {
            try
            {
                var todasLasPlantillas = GetPlantillasMock();
                var currentUser = GetCurrentUserInfo();

                // Total plantillas
                lblTotalPlantillas.Text = todasLasPlantillas.Count.ToString();

                // Este mes
                var inicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                int esteMes = todasLasPlantillas.Count(p => (DateTime)p.FechaCreacion >= inicioMes);
                lblEsteMes.Text = esteMes.ToString();

                // Mis plantillas
                int misPlantillas = todasLasPlantillas.Count(p => (int)p.IdUsuario == (currentUser?.IdUsuario ?? 0));
                lblMisPlantillas.Text = misPlantillas.ToString();

                // Servicios únicos (mock)
                lblServiciosUnicos.Text = _servicioService.ObtenerTodos().Count().ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error calculando resumen: {ex.Message}");
            }
        }

        protected void btnBuscarPlantilla_Click(object sender, EventArgs e)
        {
            CargarPlantillas();
        }

        protected void btnLimpiarPlantilla_Click(object sender, EventArgs e)
        {
            txtBuscarPlantilla.Text = "";
            ddlFiltroUsuario.SelectedIndex = 0;
            CargarPlantillas();
        }

        protected void ddlFiltroUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPlantillas();
        }

        protected void gvPlantillas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlantillas.PageIndex = e.NewPageIndex;
            CargarPlantillas();
        }

        protected void btnAccionOculta_Click(object sender, EventArgs e)
        {
            string accion = hdnAccion.Value;
            
            try
            {
                switch (accion)
                {
                    case "editar":
                        CargarDatosPlantillaParaEditar();
                        break;
                    case "ver":
                        MostrarDetallesPlantilla();
                        break;
                    case "eliminar":
                        EliminarPlantilla();
                        break;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void CargarDatosPlantillaParaEditar()
        {
            if (!int.TryParse(hdnIdPlantilla.Value, out int idPlantilla))
                return;

            var plantillas = GetPlantillasMock();
            var plantilla = plantillas.FirstOrDefault(p => (int)p.IdPlantilla == idPlantilla);
            
            if (plantilla != null)
            {
                txtNombrePlantilla.Text = plantilla.Nombre;
                txtDescripcionPlantilla.Text = plantilla.Descripcion;
                
                // Marcar servicios seleccionados (mock - seleccionar los primeros 3)
                for (int i = 0; i < Math.Min(3, cblServicios.Items.Count); i++)
                {
                    cblServicios.Items[i].Selected = true;
                }
                
                hdnAccion.Value = "editar";
                
                string script = @"
                    document.querySelector('#modalPlantillaLabel').textContent = 'Editar Plantilla';
                    setTimeout(function() {
                        actualizarContadorServicios();
                        var modal = new bootstrap.Modal(document.getElementById('modalNuevaPlantilla'));
                        modal.show();
                    }, 100);
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "showModal", script, true);
            }
        }

        private void MostrarDetallesPlantilla()
        {
            if (!int.TryParse(hdnIdPlantilla.Value, out int idPlantilla))
                return;

            var plantillas = GetPlantillasMock();
            var plantilla = plantillas.FirstOrDefault(p => (int)p.IdPlantilla == idPlantilla);
            
            if (plantilla != null)
            {
                var servicios = _servicioService.ObtenerTodos().Take(3).ToList(); // Mock de servicios asociados
                
                string detalles = $@"
                    <div class='row'>
                        <div class='col-md-6'>
                            <h6>Información General</h6>
                            <p><strong>Nombre:</strong> {(string)plantilla.Nombre}</p>
                            <p><strong>Descripción:</strong> {(string)plantilla.Descripcion}</p>
                            <p><strong>Creado por:</strong> {(string)plantilla.Usuario.Nombre} {(string)plantilla.Usuario.ApPaterno}</p>
                            <p><strong>Fecha de creación:</strong> {((DateTime)plantilla.FechaCreacion).ToString("dd/MM/yyyy")}</p>
                        </div>
                        <div class='col-md-6'>
                            <h6>Servicios Incluidos ({servicios.Count})</h6>
                            <ul class='list-group'>";
                
                foreach (var servicio in servicios)
                {
                    detalles += $"<li class='list-group-item'><i class='fas fa-check text-success'></i> {servicio.Nombre}</li>";
                }
                
                detalles += @"
                            </ul>
                        </div>
                    </div>";
                
                string script = $@"
                    document.getElementById('detallesPlantilla').innerHTML = `{detalles}`;
                    var modal = new bootstrap.Modal(document.getElementById('modalVerPlantilla'));
                    modal.show();
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "showDetalles", script, true);
            }
        }

        private void EliminarPlantilla()
        {
            if (!int.TryParse(hdnIdPlantilla.Value, out int idPlantilla))
                return;

            // Aquí implementarías la lógica real de eliminación
            // Por ahora solo mostramos un mensaje
            
            CargarPlantillas();
            CalcularResumen();
            
            ScriptManager.RegisterStartupScript(this, GetType(), "success",
                "alert('Plantilla eliminada exitosamente.');", true);
        }

        protected void btnGuardarPlantilla_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                bool esEdicion = hdnAccion.Value == "editar";
                
                // Obtener servicios seleccionados
                var serviciosSeleccionados = new List<int>();
                foreach (ListItem item in cblServicios.Items)
                {
                    if (item.Selected && int.TryParse(item.Value, out int idServicio))
                    {
                        serviciosSeleccionados.Add(idServicio);
                    }
                }

                if (serviciosSeleccionados.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error",
                        "alert('Debe seleccionar al menos un servicio para la plantilla.');", true);
                    return;
                }

                // Crear objeto plantilla
                var plantilla = new
                {
                    Nombre = txtNombrePlantilla.Text.Trim(),
                    Descripcion = txtDescripcionPlantilla.Text.Trim(),
                    FechaCreacion = DateTime.Now,
                    IdUsuario = AuthHelper.GetCurrentUserId() ?? 1,
                    Servicios = serviciosSeleccionados
                };

                // Aquí implementarías la lógica real de guardado
                // Por ahora solo simulamos el guardado exitoso

                LimpiarFormularioPlantilla();
                CargarPlantillas();
                CalcularResumen();
                
                string mensaje = esEdicion ? "Plantilla actualizada exitosamente." : "Plantilla creada exitosamente.";
                string script = $@"
                    alert('{mensaje}');
                    var modal = bootstrap.Modal.getInstance(document.getElementById('modalNuevaPlantilla'));
                    if (modal) modal.hide();
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "success", script, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al guardar plantilla: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void LimpiarFormularioPlantilla()
        {
            txtNombrePlantilla.Text = "";
            txtDescripcionPlantilla.Text = "";
            foreach (ListItem item in cblServicios.Items)
            {
                item.Selected = false;
            }
            hdnAccion.Value = "";
            hdnIdPlantilla.Value = "";
        }

        // Métodos helper para el GridView
        protected string GetCantidadServicios(object idPlantilla)
        {
            if (idPlantilla == null) return "0";
            
            // Mock: retornar un número aleatorio entre 2 y 8
            Random rand = new Random();
            return rand.Next(2, 9).ToString();
        }

        protected string GetBotonesPlantilla(object idPlantilla)
        {
            if (idPlantilla == null) return "";
            
            int id = Convert.ToInt32(idPlantilla);
            return $@"
                <button type='button' class='btn btn-sm btn-outline-info' onclick='verPlantilla({id})'>
                    <i class='fas fa-eye'></i> Ver
                </button>
                <button type='button' class='btn btn-sm btn-outline-primary ms-1' onclick='editarPlantilla({id})'>
                    <i class='fas fa-edit'></i> Editar
                </button>
                <button type='button' class='btn btn-sm btn-outline-danger ms-1' onclick='eliminarPlantilla({id})'>
                    <i class='fas fa-trash'></i> Eliminar
                </button>";
        }

        private dynamic GetCurrentUserInfo()
        {
            var userId = AuthHelper.GetCurrentUserId();
            var userFullName = AuthHelper.GetCurrentUserFullName();
            
            // Crear un objeto con la información del usuario actual
            return new
            {
                IdUsuario = userId ?? 1,
                Nombre = userFullName?.Split(' ')[0] ?? "Admin",
                ApPaterno = userFullName?.Split(' ').Length > 1 ? userFullName.Split(' ')[1] : "Sistema"
            };
        }

        private string EscapeForJavaScript(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";
            
            return input.Replace("'", "\\'").Replace("\"", "\\\"").Replace("\r", "").Replace("\n", "\\n");
        }
    }
}