using System;
using pr_proyecto.Helpers;

namespace pr_proyecto.Views
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar autenticación
            AuthHelper.RequireAuthentication();

            if (!IsPostBack)
            {
                CargarInformacionUsuario();
            }
        }

        private void CargarInformacionUsuario()
        {
            try
            {
                // Obtener información del usuario desde la sesión
                var userId = AuthHelper.GetCurrentUserId();
                var username = AuthHelper.GetCurrentUsername();
                var fullName = AuthHelper.GetCurrentUserFullName();
                var role = AuthHelper.GetCurrentUserRole();

                // Mostrar información en los labels
                lblIdUsuario.Text = userId?.ToString() ?? "N/A";
                lblUsuario.Text = username ?? "N/A";
                lblNombre.Text = fullName ?? "N/A";
                lblRol.Text = role ?? "N/A";

                // Configurar funcionalidades según el rol
                ConfigurarFuncionalidadesSegunRol(role);
            }
            catch (Exception ex)
            {
                // En caso de error, redirigir al login
                Response.Redirect("~/Views/Login.aspx");
            }
        }

        private void ConfigurarFuncionalidadesSegunRol(string rol)
        {
            // Ocultar todos los paneles primero
            panelVendedor.Visible = false;
            panelAdmin.Visible = false;

            // Mostrar panel según el rol
            if (rol != null)
            {
                switch (rol.ToLower())
                {
                    case "vendedor":
                        panelVendedor.Visible = true;
                        break;
                    case "administrador":
                    case "admin":
                        panelAdmin.Visible = true;
                        panelVendedor.Visible = true; // Admin puede ver funciones de vendedor también
                        break;
                    default:
                        // Por defecto, mostrar funciones básicas de vendedor
                        panelVendedor.Visible = true;
                        break;
                }
            }
        }
    }
}