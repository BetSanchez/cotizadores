using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using pr_proyecto.Data;
using pr_proyecto.Models;
using pr_proyecto.Repository;
using pr_proyecto.Services;

namespace pr_proyecto
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        private AppDbContext _db;
        private UsuarioService _service;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Inicializa DbContext → Repo → Service
            _db      = new AppDbContext();
            var repo = new UsuarioRepository(_db);
            _service = new UsuarioService(repo);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles();
                CargarEstatus();
            }
        }

        private void CargarRoles()
        {
            ddlRol.Items.Clear();
            ddlRol.Items.Add(new ListItem("-- Seleccione rol --", ""));
            foreach (var rol in _db.Roles.OrderBy(r => r.Nombre))
            {
                ddlRol.Items.Add(new ListItem(rol.Nombre, rol.IdRol.ToString()));
            }
        }

        private void CargarEstatus()
        {
            DropDownList1.Items.Clear();
            DropDownList1.Items.Add(new ListItem("-- Seleccione estatus --", ""));
            DropDownList1.Items.Add(new ListItem("Activo", "true"));
            DropDownList1.Items.Add(new ListItem("Inactivo", "false"));
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                var usuario = new Usuario
                {
                    Nombre          = txtNombre.Text.Trim(),
                    ApPaterno       = txtApPaterno.Text.Trim(),
                    ApMaterno       = txtApMaterno.Text.Trim(),
                    Correo          = txtCorreo.Text.Trim(),
                    Telefono        = txtTelefono.Text.Trim(),
                    TelEmpresa      = txtTelEmpresa.Text.Trim(),
                    Carrera         = txtCarrera.Text.Trim(),
                    Direccion       = txtDireccion.Text.Trim(),
                    FechaActivacion = DateTime.Parse(txtFechaActivacion.Text),
                    FechaIngreso    = DateTime.TryParse(txtFechaIngreso.Text, out var fi) ? fi : (DateTime?)null,
                    FechaTermino    = DateTime.TryParse(txtFechaTermino.Text, out var ft) ? ft : (DateTime?)null,
                    NomUsuario      = txtNomUsuario.Text.Trim(),
                    Contraseña      = txtContrasena.Text.Trim(),  // considera encriptar aquí
                    Estatus         = bool.Parse(DropDownList1.SelectedValue),
                    IdRol           = int.Parse(ddlRol.SelectedValue)
                };

                _service.Registrar(usuario);
                // Al éxito, redirige al listado de usuarios/clientes
                Response.Redirect("~/UsuariosList.aspx");
            }
            catch (Exception ex)
            {
                ValidationSummary1.HeaderText = "Error al registrar:";
                ValidationSummary1.Controls.Add(new LiteralControl(ex.Message));
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            _db.Dispose();
        }
    }
}
