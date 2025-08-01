using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using pr_proyecto.Data;
using pr_proyecto.Models;
using pr_proyecto.Repository;
using pr_proyecto.Services;

namespace pr_proyecto
{
    public partial class WebForm3 : System.Web.UI.Page
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
            // Limpiar mensajes anteriores
            LimpiarMensajes();

            if (!Page.IsValid) 
            {
                MostrarError("Por favor, corrija los errores de validación antes de continuar.");
                return;
            }

            try
            {
                // Validar que los dropdowns tengan valores seleccionados
                if (string.IsNullOrEmpty(ddlRol.SelectedValue))
                {
                    MostrarError("Debe seleccionar un rol de usuario.");
                    return;
                }

                if (string.IsNullOrEmpty(DropDownList1.SelectedValue))
                {
                    MostrarError("Debe seleccionar un estatus.");
                    return;
                }

                // Crear el objeto usuario
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
                    Contrasena = txtContrasena.Text.Trim(),  
                    Estatus         = bool.Parse(DropDownList1.SelectedValue),
                    IdRol           = int.Parse(ddlRol.SelectedValue)
                };

                // Intentar registrar el usuario
                _service.Registrar(usuario);
                
                // Si llegamos aquí, el registro fue exitoso
                MostrarExito($"Usuario '{usuario.NomUsuario}' registrado exitosamente.");
                
                // Limpiar el formulario después de un registro exitoso
                LimpiarFormulario();
                
                // Opcional: Redirigir después de un breve delay
                // Response.Redirect("~/Views/UsuariosList.aspx");
            }
            catch (ArgumentException argEx)
            {
                // Error de validación de negocio
                MostrarError($"Error de validación: {argEx.Message}");
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
            {
                // Error específico de Entity Framework
                System.Diagnostics.Debug.WriteLine($"AltaVendedor.btnGuardar_Click: EntityCommandExecutionException: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"AltaVendedor.btnGuardar_Click: Inner Exception: {ex.InnerException?.Message}");
                MostrarError("Error de conexión con la base de datos. Por favor, verifica que el servidor MySQL esté ejecutándose.");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                // Error específico de SQL
                System.Diagnostics.Debug.WriteLine($"AltaVendedor.btnGuardar_Click: SqlException: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"AltaVendedor.btnGuardar_Click: Error Number: {ex.Number}");
                MostrarError("Error de base de datos. Por favor, contacta al administrador del sistema.");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException dbEx)
            {
                // Error de base de datos (restricciones, duplicados, etc.)
                System.Diagnostics.Debug.WriteLine($"AltaVendedor.btnGuardar_Click: DbUpdateException: {dbEx.Message}");
                System.Diagnostics.Debug.WriteLine($"AltaVendedor.btnGuardar_Click: Inner Exception: {dbEx.InnerException?.Message}");
                MostrarError($"Error en la base de datos: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                // Error general
                System.Diagnostics.Debug.WriteLine($"AltaVendedor.btnGuardar_Click: Error inesperado: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"AltaVendedor.btnGuardar_Click: Stack Trace: {ex.StackTrace}");
                MostrarError($"Error inesperado: {ex.Message}");
            }
        }

        private void MostrarExito(string mensaje)
        {
            pnlMensajes.CssClass = "alert alert-success";
            pnlMensajes.Controls.Clear();
            
            var icono = new HtmlGenericControl("i");
            icono.Attributes["class"] = "fas fa-check-circle";
            icono.Attributes["style"] = "margin-right: 8px;";
            
            pnlMensajes.Controls.Add(icono);
            pnlMensajes.Controls.Add(new LiteralControl(mensaje));
            pnlMensajes.Visible = true;
        }

        private void MostrarError(string mensaje)
        {
            pnlMensajes.CssClass = "alert alert-danger";
            pnlMensajes.Controls.Clear();
            
            var icono = new HtmlGenericControl("i");
            icono.Attributes["class"] = "fas fa-exclamation-triangle";
            icono.Attributes["style"] = "margin-right: 8px;";
            
            pnlMensajes.Controls.Add(icono);
            pnlMensajes.Controls.Add(new LiteralControl(mensaje));
            pnlMensajes.Visible = true;
        }

        private void LimpiarMensajes()
        {
            pnlMensajes.Visible = false;
            pnlMensajes.Controls.Clear();
        }

        private void LimpiarFormulario()
        {
            // Limpiar todos los campos del formulario
            txtNombre.Text = "";
            txtApPaterno.Text = "";
            txtApMaterno.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
            txtTelEmpresa.Text = "";
            txtCarrera.Text = "";
            txtDireccion.Text = "";
            txtFechaIngreso.Text = "";
            txtFechaTermino.Text = "";
            txtNomUsuario.Text = "";
            txtContrasena.Text = "";
            txtConfirmarContrasena.Text = "";
            
            // Resetear dropdowns
            ddlRol.SelectedIndex = 0;
            DropDownList1.SelectedIndex = 0;
            
            // Establecer fecha actual en activación
            txtFechaActivacion.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            LimpiarFormulario();
            MostrarExito("Formulario limpiado correctamente.");
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            _db?.Dispose();
        }
    }
}