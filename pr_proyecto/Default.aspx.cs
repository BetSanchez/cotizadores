using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pr_proyecto.Helpers;

namespace pr_proyecto
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si ya hay un usuario autenticado
            if (AuthHelper.IsAuthenticated())
            {
                // Si ya está autenticado, redirigir al dashboard
                Response.Redirect("~/Views/Dashboard.aspx");
            }
            else
            {
                // Si no está autenticado, redirigir al login
                Response.Redirect("~/Views/Login.aspx");
            }
        }
    }
}