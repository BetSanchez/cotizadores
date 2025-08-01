using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pr_proyecto.Data;
using pr_proyecto.Models;
using pr_proyecto.Repository;
using pr_proyecto.Services;

namespace pr_proyecto.Views
{
    public partial class Login : System.Web.UI.Page
    {
        private AppDbContext _db;
        private UsuarioService _service;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Inicializa DbContext → Repo → Service
            _db = new AppDbContext();
            var repo = new UsuarioRepository(_db);
            _service = new UsuarioService(repo);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si ya hay una sesión activa
            if (Session["UsuarioId"] != null)
            {
                Response.Redirect("~/Views/Dashboard.aspx");
            }

            if (!IsPostBack)
            {
                // Limpiar cualquier mensaje de error previo
                ValidationSummary1.Visible = false;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Login.btnLogin_Click: Iniciando proceso de login");
                
                // Validar que los campos no estén vacíos
                if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text))
                {
                    MostrarError("Por favor, completa todos los campos.");
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"Login.btnLogin_Click: Intentando autenticar usuario: {txtUsuario.Text.Trim()}");

                // Intentar autenticar al usuario
                var usuario = _service.Autenticar(txtUsuario.Text.Trim(), txtContrasena.Text);

                if (usuario != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Login.btnLogin_Click: Usuario autenticado exitosamente. ID: {usuario.IdUsuario}");
                    
                    // Crear sesión del usuario
                    Session["UsuarioId"] = usuario.IdUsuario;
                    Session["NombreUsuario"] = usuario.NomUsuario;
                    Session["NombreCompleto"] = $"{usuario.Nombre} {usuario.ApPaterno} {usuario.ApMaterno}";
                    Session["Rol"] = usuario.Rol?.Nombre ?? "Sin rol";
                    Session["RolId"] = usuario.IdRol;

                    // Si marcó "recordar sesión", crear una cookie
                    if (chkRecordar.Checked)
                    {
                        HttpCookie cookie = new HttpCookie("UsuarioRecordado", usuario.IdUsuario.ToString());
                        cookie.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Add(cookie);
                    }

                    System.Diagnostics.Debug.WriteLine($"Login.btnLogin_Click: Redirigiendo usuario con rol ID: {usuario.IdRol}");

                    // Redirigir según el rol del usuario
                    RedirigirSegunRol(usuario.IdRol);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Login.btnLogin_Click: Usuario no encontrado");
                    MostrarError("Credenciales inválidas. Verifica tu usuario y contraseña.");
                }
            }
            catch (ArgumentException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Login.btnLogin_Click: ArgumentException: {ex.Message}");
                MostrarError(ex.Message);
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Login.btnLogin_Click: EntityCommandExecutionException: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Login.btnLogin_Click: Inner Exception: {ex.InnerException?.Message}");
                MostrarError("Error de conexión con la base de datos. Por favor, verifica que el servidor MySQL esté ejecutándose.");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Login.btnLogin_Click: SqlException: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Login.btnLogin_Click: Error Number: {ex.Number}");
                MostrarError("Error de base de datos. Por favor, contacta al administrador del sistema.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Login.btnLogin_Click: Error inesperado: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Login.btnLogin_Click: Stack Trace: {ex.StackTrace}");
                MostrarError("Error interno del sistema. Por favor, intenta nuevamente.");
            }
        }

        private void MostrarError(string mensaje)
        {
            ValidationSummary1.Visible = true;
            ValidationSummary1.HeaderText = mensaje;
        }

        private void RedirigirSegunRol(int rolId)
        {
            // Aquí puedes personalizar las redirecciones según el rol
            // Por ahora, redirigimos al Dashboard
            Response.Redirect("~/Views/Dashboard.aspx");
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (_db != null)
            {
                _db.Dispose();
            }
        }
    }
}