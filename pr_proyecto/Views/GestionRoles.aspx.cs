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
    public partial class GestionRoles : System.Web.UI.Page
    {
        private readonly IRolService _rolService;
        private readonly IUsuarioService _usuarioService;

        public GestionRoles()
        {
            var context = new Data.AppDbContext();
            _rolService = new RolService(new Repository.RolRepository(context));
            _usuarioService = new UsuarioService(new Repository.UsuarioRepository(context));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthHelper.RequireAuthentication();
            
            // Solo administradores pueden gestionar roles
            var roleId = AuthHelper.GetCurrentUserRoleId();
            if (roleId != 1) // 1 = Administrador
            {
                Response.Redirect("~/Views/Dashboard.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarRoles();
                CargarPermisos();
                CargarPermisosParaAsignar();
            }
        }

        private void CargarRoles()
        {
            try
            {
                var roles = _rolService.ObtenerTodos().ToList();
                var rolesFiltrados = AplicarFiltrosRoles(roles);
                
                gvRoles.DataSource = rolesFiltrados;
                gvRoles.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al cargar roles: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void CargarPermisos()
        {
            try
            {
                var permisos = _rolService.ObtenerTodosPermisos().ToList();
                
                gvPermisos.DataSource = permisos;
                gvPermisos.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al cargar permisos: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void CargarPermisosParaAsignar()
        {
            try
            {
                var permisos = _rolService.ObtenerTodosPermisos().ToList();
                
                cblPermisos.DataSource = permisos;
                cblPermisos.DataTextField = "Nombre";
                cblPermisos.DataValueField = "IdPrivilegio";
                cblPermisos.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al cargar permisos para asignar: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private List<Rol> AplicarFiltrosRoles(List<Rol> roles)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarRol.Text))
                return roles;

            string termino = txtBuscarRol.Text.Trim().ToLower();
            return roles.Where(r => 
                (r.Nombre?.ToLower().Contains(termino) ?? false)
            ).ToList();
        }

        protected void btnBuscarRol_Click(object sender, EventArgs e)
        {
            CargarRoles();
        }

        protected void btnLimpiarRol_Click(object sender, EventArgs e)
        {
            txtBuscarRol.Text = "";
            CargarRoles();
        }

        protected void gvRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRoles.PageIndex = e.NewPageIndex;
            CargarRoles();
        }

        protected void btnAccionOculta_Click(object sender, EventArgs e)
        {
            string accion = hdnAccion.Value;
            
            try
            {
                switch (accion)
                {
                    case "editar_rol":
                        CargarDatosRolParaEditar();
                        break;
                    case "eliminar_rol":
                        EliminarRol();
                        break;
                    case "eliminar_permiso":
                        EliminarPermiso();
                        break;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void CargarDatosRolParaEditar()
        {
            if (!int.TryParse(hdnIdRol.Value, out int idRol))
                return;

            var rol = _rolService.ObtenerPorId(idRol);
            if (rol != null)
            {
                txtNombreRol.Text = rol.Nombre;
                
                // Cargar permisos del rol
                var permisosRol = _rolService.ObtenerPermisosPorRol(idRol);
                foreach (ListItem item in cblPermisos.Items)
                {
                    if (int.TryParse(item.Value, out int idPermiso))
                    {
                        item.Selected = permisosRol.Any(p => p.IdPrivilegio == idPermiso);
                    }
                }
                
                hdnAccion.Value = "editar_rol";
                
                string script = @"
                    document.querySelector('#modalRolLabel').textContent = 'Editar Rol';
                    var modal = new bootstrap.Modal(document.getElementById('modalNuevoRol'));
                    modal.show();
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "showModal", script, true);
            }
        }

        private void EliminarRol()
        {
            if (!int.TryParse(hdnIdRol.Value, out int idRol))
                return;

            // Verificar que no sea un rol del sistema
            if (idRol <= 3) // Administrador, Vendedor, Cliente son roles del sistema
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    "alert('No se pueden eliminar los roles del sistema.');", true);
                return;
            }

            _rolService.Eliminar(idRol);
            CargarRoles();
            
            ScriptManager.RegisterStartupScript(this, GetType(), "success",
                "alert('Rol eliminado exitosamente.');", true);
        }

        private void EliminarPermiso()
        {
            if (!int.TryParse(hdnIdPermiso.Value, out int idPermiso))
                return;

            _rolService.EliminarPermiso(idPermiso);
            CargarPermisos();
            CargarPermisosParaAsignar();
            
            ScriptManager.RegisterStartupScript(this, GetType(), "success",
                "alert('Permiso eliminado exitosamente.');", true);
        }

        protected void btnGuardarRol_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                bool esEdicion = hdnAccion.Value == "editar_rol";
                Rol rol;

                if (esEdicion)
                {
                    int idRol = Convert.ToInt32(hdnIdRol.Value);
                    rol = _rolService.ObtenerPorId(idRol);
                    if (rol == null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "error",
                            "alert('Rol no encontrado.');", true);
                        return;
                    }
                }
                else
                {
                    rol = new Rol();
                }

                rol.Nombre = txtNombreRol.Text.Trim();

                if (esEdicion)
                {
                    _rolService.Actualizar(rol);
                }
                else
                {
                    _rolService.Agregar(rol);
                }

                // Asignar permisos seleccionados
                var permisosSeleccionados = new List<int>();
                foreach (ListItem item in cblPermisos.Items)
                {
                    if (item.Selected && int.TryParse(item.Value, out int idPermiso))
                    {
                        permisosSeleccionados.Add(idPermiso);
                    }
                }

                _rolService.AsignarPermisos(rol.IdRol, permisosSeleccionados);

                LimpiarFormularioRol();
                CargarRoles();
                
                string mensaje = esEdicion ? "Rol actualizado exitosamente." : "Rol creado exitosamente.";
                string script = $@"
                    alert('{mensaje}');
                    var modal = bootstrap.Modal.getInstance(document.getElementById('modalNuevoRol'));
                    if (modal) modal.hide();
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "success", script, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al guardar rol: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        protected void btnGuardarPermiso_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                var permiso = new Privilegio
                {
                    Nombre = txtNombrePermiso.Text.Trim(),
                    Valor = Convert.ToInt32(txtCodigoPermiso.Text.Trim())
                };

                _rolService.CrearPermiso(permiso);
                
                LimpiarFormularioPermiso();
                CargarPermisos();
                CargarPermisosParaAsignar();
                
                ScriptManager.RegisterStartupScript(this, GetType(), "success",
                    "alert('Permiso creado exitosamente.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al crear permiso: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void LimpiarFormularioRol()
        {
            txtNombreRol.Text = "";
            foreach (ListItem item in cblPermisos.Items)
            {
                item.Selected = false;
            }
            hdnAccion.Value = "";
            hdnIdRol.Value = "";
        }

        private void LimpiarFormularioPermiso()
        {
            txtNombrePermiso.Text = "";
            txtCodigoPermiso.Text = "";
        }

        // Métodos helper para el GridView
        protected string GetCantidadPermisos(object idRol)
        {
            if (idRol == null) return "0";
            
            try
            {
                int id = Convert.ToInt32(idRol);
                var permisos = _rolService.ObtenerPermisosPorRol(id);
                return permisos.Count().ToString();
            }
            catch
            {
                return "0";
            }
        }

        protected string GetCantidadUsuarios(object idRol)
        {
            if (idRol == null) return "0";
            
            try
            {
                int id = Convert.ToInt32(idRol);
                var usuarios = _usuarioService.ObtenerTodos().Where(u => u.IdRol == id);
                return usuarios.Count().ToString();
            }
            catch
            {
                return "0";
            }
        }

        protected string GetBotonesRol(object idRol)
        {
            if (idRol == null) return "";
            
            int id = Convert.ToInt32(idRol);
            string botones = $@"
                <button type='button' class='btn btn-sm btn-outline-primary' onclick='editarRol({id})'>
                    <i class='fas fa-edit'></i> Editar
                </button>";

            // Solo permitir eliminar roles que no sean del sistema
            if (id > 3)
            {
                botones += $@"
                <button type='button' class='btn btn-sm btn-outline-danger ms-1' onclick='eliminarRol({id})'>
                    <i class='fas fa-trash'></i> Eliminar
                </button>";
            }

            return botones;
        }

        protected string GetBotonesPermiso(object idPermiso)
        {
            if (idPermiso == null) return "";
            
            int id = Convert.ToInt32(idPermiso);
            return $@"
                <button type='button' class='btn btn-sm btn-outline-danger' onclick='eliminarPermiso({id})'>
                    <i class='fas fa-trash'></i> Eliminar
                </button>";
        }

        private string EscapeForJavaScript(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";
            
            return input.Replace("'", "\\'").Replace("\"", "\\\"").Replace("\r", "").Replace("\n", "\\n");
        }
    }
}