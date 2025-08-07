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
    public partial class VerContactos : System.Web.UI.Page
    {
        private readonly IContactoService _contactoService;
        private readonly ISucursalService _sucursalService;
        private List<Contacto> _contactosFiltrados;

        public VerContactos()
        {
            var contactoRepository = new ContactoRepository(new Data.AppDbContext());
            var sucursalRepository = new SucursalRepository(new Data.AppDbContext());
            _contactoService = new ContactoService(contactoRepository);
            _sucursalService = new SucursalService(sucursalRepository);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar autenticación
            AuthHelper.RequireAuthentication();

            if (!IsPostBack)
            {
                CargarContactos();
                CargarDropDownSucursales();
                CalcularResumen();
            }
        }

        private void CargarContactos()
        {
            try
            {
                var todosLosContactos = _contactoService.ObtenerTodos().ToList();
                _contactosFiltrados = AplicarFiltros(todosLosContactos);
                
                gvContactos.DataSource = _contactosFiltrados;
                gvContactos.DataBind();

                CalcularResumen();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al cargar contactos: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void CargarDropDownSucursales()
        {
            try
            {
                var sucursales = _sucursalService.ObtenerTodos().ToList();
                
                // Para filtro
                ddlFiltroSucursal.Items.Clear();
                ddlFiltroSucursal.Items.Add(new ListItem("Todas las sucursales", ""));
                foreach (var sucursal in sucursales)
                {
                    ddlFiltroSucursal.Items.Add(new ListItem($"{sucursal.NomComercial} ({sucursal.Empresa?.Nombre})", sucursal.IdSucursal.ToString()));
                }

                // Para modal
                ddlSucursalContacto.Items.Clear();
                ddlSucursalContacto.Items.Add(new ListItem("Seleccione una sucursal", ""));
                foreach (var sucursal in sucursales)
                {
                    ddlSucursalContacto.Items.Add(new ListItem($"{sucursal.NomComercial} ({sucursal.Empresa?.Nombre})", sucursal.IdSucursal.ToString()));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cargando dropdown sucursales: {ex.Message}");
            }
        }

        private List<Contacto> AplicarFiltros(List<Contacto> contactos)
        {
            var filtrados = contactos.AsQueryable();

            // Filtro por texto
            if (!string.IsNullOrWhiteSpace(txtBuscarContacto.Text))
            {
                string termino = txtBuscarContacto.Text.Trim().ToLower();
                filtrados = filtrados.Where(c =>
                   (c.Nombre != null && c.Nombre.ToLower().Contains(termino)) ||
    (c.ApPaterno != null && c.ApPaterno.ToLower().Contains(termino)) ||
    (c.ApMaterno != null && c.ApMaterno.ToLower().Contains(termino)) ||
    (c.Correo != null && c.Correo.ToLower().Contains(termino))
                );
            }

            // Filtro por sucursal
            if (!string.IsNullOrEmpty(ddlFiltroSucursal.SelectedValue))
            {
                int sucursalId = Convert.ToInt32(ddlFiltroSucursal.SelectedValue);
                filtrados = filtrados.Where(c => c.IdSucursal == sucursalId);
            }

                            // Filtro por tipo eliminado - campo no existe en BD

            return filtrados.ToList();
        }

        private void CalcularResumen()
        {
            try
            {
                var todosLosContactos = _contactoService.ObtenerTodos().ToList();

                // Total contactos
                lblTotalContactos.Text = todosLosContactos.Count.ToString();

                // Estadísticas por tipo eliminadas - campo Tipo no existe en BD

                // Este mes - sin campo FechaRegistro, se usa cantidad total
                lblEsteMes.Text = todosLosContactos.Count().ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error calculando resumen: {ex.Message}");
            }
        }

        protected void btnBuscarContacto_Click(object sender, EventArgs e)
        {
            CargarContactos();
        }

        protected void btnLimpiarContacto_Click(object sender, EventArgs e)
        {
            txtBuscarContacto.Text = "";
            ddlFiltroSucursal.SelectedIndex = 0;
            // ddlFiltroTipo eliminado - campo no existe en BD
            CargarContactos();
        }

        protected void ddlFiltroSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarContactos();
        }

        // ddlFiltroTipo_SelectedIndexChanged eliminado - campo no existe en BD

        protected void gvContactos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvContactos.PageIndex = e.NewPageIndex;
            CargarContactos();
        }

        protected void btnAccionOculto_Click(object sender, EventArgs e)
        {
            try
            {
                string accion = hdnAccion.Value;
                
                switch (accion)
                {
                    case "eliminar":
                        EliminarContacto();
                        break;
                    case "cargar_editar":
                        CargarContactoParaEditar();
                        break;
                }

                // Limpiar campos ocultos
                hdnAccion.Value = "";
                hdnIdContacto.Value = "";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void EliminarContacto()
        {
            if (string.IsNullOrEmpty(hdnIdContacto.Value))
                return;

            int idContacto = Convert.ToInt32(hdnIdContacto.Value);
            var contacto = _contactoService.ObtenerPorId(idContacto);
            
            if (contacto != null)
            {
                _contactoService.Eliminar(idContacto);
                CargarContactos();
                
                ScriptManager.RegisterStartupScript(this, GetType(), "success",
                    $"alert('Contacto \"{EscapeForJavaScript(contacto.Nombre)} {EscapeForJavaScript(contacto.ApPaterno)}\" eliminado correctamente.');", true);
            }
        }

        private void CargarContactoParaEditar()
        {
            if (string.IsNullOrEmpty(hdnIdContacto.Value))
                return;

            int idContacto = Convert.ToInt32(hdnIdContacto.Value);
            var contacto = _contactoService.ObtenerPorId(idContacto);
            
            if (contacto != null)
            {
                txtNombreContacto.Text = contacto.Nombre;
                txtApPaternoContacto.Text = contacto.ApPaterno;
                txtApMaternoContacto.Text = contacto.ApMaterno ?? "";
                ddlSucursalContacto.SelectedValue = contacto.IdSucursal.ToString();
                txtPuestoContacto.Text = contacto.Puesto ?? "";
                txtTelefonoContacto.Text = contacto.Telefono ?? "";
                txtEmailContacto.Text = contacto.Correo ?? "";
                // Campos Tipo y Observaciones eliminados - no existen en BD
                
                hdnAccion.Value = "editar";
                
                string script = @"
                    document.querySelector('#modalContactoLabel').textContent = 'Editar Contacto';
                    var modal = new bootstrap.Modal(document.getElementById('modalNuevoContacto'));
                    modal.show();
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "showEditModal", script, true);
            }
        }

        protected void btnGuardarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreContacto.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error",
                        "alert('El nombre del contacto es requerido.');", true);
                    return;
                }

                if (string.IsNullOrEmpty(ddlSucursalContacto.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error",
                        "alert('Debe seleccionar una sucursal.');", true);
                    return;
                }

                bool esEdicion = hdnAccion.Value == "editar" && !string.IsNullOrEmpty(hdnIdContacto.Value);
                
                Contacto contacto;
                if (esEdicion)
                {
                    int idContacto = Convert.ToInt32(hdnIdContacto.Value);
                    contacto = _contactoService.ObtenerPorId(idContacto);
                    if (contacto == null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "error",
                            "alert('Contacto no encontrado.');", true);
                        return;
                    }
                }
                else
                {
                    contacto = new Contacto();
                    // FechaRegistro no existe en BD, se elimina
                }

                contacto.Nombre = txtNombreContacto.Text.Trim();
                contacto.ApPaterno = txtApPaternoContacto.Text.Trim();
                contacto.ApMaterno = txtApMaternoContacto.Text.Trim();
                contacto.IdSucursal = Convert.ToInt32(ddlSucursalContacto.SelectedValue);
                contacto.Puesto = txtPuestoContacto.Text.Trim();
                contacto.Telefono = txtTelefonoContacto.Text.Trim();
                contacto.Correo = txtEmailContacto.Text.Trim();

                if (esEdicion)
                {
                    _contactoService.Actualizar(contacto);
                }
                else
                {
                    _contactoService.Registrar(contacto);
                }

                LimpiarFormulario();
                CargarContactos();

                string action = esEdicion ? "actualizado" : "creado";
                string script = $@"
                    var modal = bootstrap.Modal.getInstance(document.getElementById('modalNuevoContacto'));
                    if (modal) modal.hide();
                    alert('Contacto {action} correctamente.');
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "success", script, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al guardar contacto: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void LimpiarFormulario()
        {
            txtNombreContacto.Text = "";
            txtApPaternoContacto.Text = "";
            txtApMaternoContacto.Text = "";
            ddlSucursalContacto.SelectedIndex = 0;
            txtPuestoContacto.Text = "";
            txtTelefonoContacto.Text = "";
            txtEmailContacto.Text = "";
            // ddlTipoContacto eliminado - campo no existe en BD
            hdnAccion.Value = "";
            hdnIdContacto.Value = "";
        }

                // Métodos helper para el GridView
        // GetTipoBadgeClass eliminado - campo Tipo no existe en BD

        protected string GetBotonesContacto(object idContacto)
        {
            if (idContacto == null) return "";
            
            return $@"
                <button type='button' class='btn btn-info btn-sm me-1' onclick='verDetalleContacto({idContacto})'>
                    <i class='fas fa-eye'></i> Ver
                </button>
                <button type='button' class='btn btn-primary btn-sm me-1' onclick='editarContacto({idContacto})'>
                    <i class='fas fa-edit'></i> Editar
                </button>
                <button type='button' class='btn btn-danger btn-sm' onclick='eliminarContacto({idContacto}, ""Contacto"")'>
                    <i class='fas fa-trash'></i> Eliminar
                </button>
            ";
        }

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