using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pr_proyecto.Views
{
    public partial class VerUsuarios : System.Web.UI.Page
    {
        private static List<UsuarioMock> _usuarios = new List<UsuarioMock>
        {
            new UsuarioMock { IdUsuario = 1, Nombre = "Juan Pérez", Email = "juan@correo.com", Rol = "Administrador", Activo = true },
            new UsuarioMock { IdUsuario = 2, Nombre = "Ana López", Email = "ana@correo.com", Rol = "Vendedor", Activo = true },
            new UsuarioMock { IdUsuario = 3, Nombre = "Carlos Ruiz", Email = "carlos@correo.com", Rol = "Cliente", Activo = false }
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            var lista = _usuarios;
            if (!string.IsNullOrWhiteSpace(txtBuscarUsuario.Text))
            {
                string termino = txtBuscarUsuario.Text.Trim().ToLower();
                lista = lista.Where(u => u.Nombre.ToLower().Contains(termino) || u.Email.ToLower().Contains(termino)).ToList();
            }
            gvUsuarios.DataSource = lista;
            gvUsuarios.DataBind();
        }

        protected void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        protected void btnLimpiarUsuario_Click(object sender, EventArgs e)
        {
            txtBuscarUsuario.Text = "";
            CargarUsuarios();
        }

        protected void gvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsuarios.PageIndex = e.NewPageIndex;
            CargarUsuarios();
        }

        protected void btnAccionOculto_Click(object sender, EventArgs e)
        {
            string accion = hdnAccion.Value;
            int idUsuario = string.IsNullOrEmpty(hdnIdUsuario.Value) ? 0 : Convert.ToInt32(hdnIdUsuario.Value);
            switch (accion)
            {
                case "baja_usuario":
                    CambiarEstadoUsuario(idUsuario, false);
                    break;
                case "alta_usuario":
                    CambiarEstadoUsuario(idUsuario, true);
                    break;
                case "cargar_editar_usuario":
                    CargarUsuarioParaEditar(idUsuario);
                    break;
            }
            hdnAccion.Value = "";
            hdnIdUsuario.Value = "";
        }

        private void CambiarEstadoUsuario(int idUsuario, bool activo)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
            if (usuario != null)
            {
                usuario.Activo = activo;
                CargarUsuarios();
            }
        }

        private void CargarUsuarioParaEditar(int idUsuario)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
            if (usuario != null)
            {
                txtNombreUsuario.Text = usuario.Nombre;
                txtEmailUsuario.Text = usuario.Email;
                ddlRolUsuario.SelectedValue = usuario.Rol;
                chkActivo.Checked = usuario.Activo;
                hdnAccion.Value = "editar_usuario";
                hdnIdUsuario.Value = usuario.IdUsuario.ToString();
                string script = @"document.querySelector('#modalUsuarioLabel').textContent = 'Editar Usuario';var modal = new bootstrap.Modal(document.getElementById('modalNuevoUsuario'));modal.show();";
                ScriptManager.RegisterStartupScript(this, GetType(), "showEditModal", script, true);
            }
        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            bool esEdicion = hdnAccion.Value == "editar_usuario" && !string.IsNullOrEmpty(hdnIdUsuario.Value);
            UsuarioMock usuario;
            if (esEdicion)
            {
                int idUsuario = Convert.ToInt32(hdnIdUsuario.Value);
                usuario = _usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
                if (usuario == null) return;
            }
            else
            {
                usuario = new UsuarioMock();
                usuario.IdUsuario = _usuarios.Max(u => u.IdUsuario) + 1;
                _usuarios.Add(usuario);
            }
            usuario.Nombre = txtNombreUsuario.Text.Trim();
            usuario.Email = txtEmailUsuario.Text.Trim();
            usuario.Rol = ddlRolUsuario.SelectedValue;
            usuario.Activo = chkActivo.Checked;
            LimpiarFormularioUsuario();
            CargarUsuarios();
            string action = esEdicion ? "actualizado" : "creado";
            string script = $@"var modal = bootstrap.Modal.getInstance(document.getElementById('modalNuevoUsuario'));if (modal) modal.hide();alert('Usuario {action} correctamente.');";
            ScriptManager.RegisterStartupScript(this, GetType(), "success", script, true);
        }

        private void LimpiarFormularioUsuario()
        {
            txtNombreUsuario.Text = "";
            txtEmailUsuario.Text = "";
            ddlRolUsuario.SelectedIndex = 0;
            chkActivo.Checked = true;
            hdnAccion.Value = "";
            hdnIdUsuario.Value = "";
        }

        protected string GetBotonesUsuario(object idUsuario, bool activo)
        {
            int id = Convert.ToInt32(idUsuario);
            var usuario = _usuarios.FirstOrDefault(u => u.IdUsuario == id);
            string nombre = usuario != null ? usuario.Nombre : "Usuario";
            string btnEstado = activo
                ? $"<button type='button' class='btn btn-danger btn-sm' onclick=\"cambiarEstadoUsuario({id}, '{nombre}', 'baja')\"><i class='fas fa-user-slash'></i> Baja</button>"
                : $"<button type='button' class='btn btn-success btn-sm' onclick=\"cambiarEstadoUsuario({id}, '{nombre}', 'alta')\"><i class='fas fa-user-check'></i> Alta</button>";
            return $@"<button type='button' class='btn btn-primary btn-sm me-1' onclick='editarUsuario({id})'><i class='fas fa-edit'></i> Editar</button>{btnEstado}";
        }

        public class UsuarioMock
        {
            public int IdUsuario { get; set; }
            public string Nombre { get; set; }
            public string Email { get; set; }
            public string Rol { get; set; }
            public bool Activo { get; set; }
        }
    }
}
