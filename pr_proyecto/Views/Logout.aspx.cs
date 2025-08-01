using System;
using System.Web;

namespace pr_proyecto.Views
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Limpiar todas las variables de sesión
            Session.Clear();
            Session.Abandon();

            // Eliminar la cookie de "recordar sesión" si existe
            if (Request.Cookies["UsuarioRecordado"] != null)
            {
                HttpCookie cookie = new HttpCookie("UsuarioRecordado");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            // Redirigir al login después de un breve delay
            Response.AddHeader("Refresh", "2;url=Login.aspx");
        }
    }
}