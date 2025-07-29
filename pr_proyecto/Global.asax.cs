using System;
using System.Data.Entity;
using System.Diagnostics;
using pr_proyecto.Data;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace pr_proyecto
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<AppDbContext>(null);

            try
            {
                using (var db = new AppDbContext())
                {
                    db.Database.Connection.Open();
                    Trace.TraceInformation("✅ Conexión exitosa a la base de datos MySQL.");
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("❌ Error al conectar a la base de datos MySQL: " + ex.Message);
            }
        }
    }
}