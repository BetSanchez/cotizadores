using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using pr_proyecto.Data;
using pr_proyecto.Models;

namespace pr_proyecto
{
    public partial class TestSimple : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Página de carga
        }

        protected void btnTestConexion_Click(object sender, EventArgs e)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                var providerName = ConfigurationManager.ConnectionStrings["DefaultConnection"].ProviderName;
                
                var info = $"Conexión configurada:\n" +
                          $"Provider: {providerName}\n" +
                          $"Connection String: {connectionString}\n\n";

                // Probar conexión directa
                using (var connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
                {
                    connection.Open();
                    info += "✅ Conexión directa exitosa\n";
                    
                    // Verificar que la base de datos existe
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT DATABASE()";
                        var dbName = command.ExecuteScalar()?.ToString();
                        info += $"Base de datos actual: {dbName}\n";
                    }
                    
                    connection.Close();
                }

                MostrarResultado(pnlResultConexion, info, true);
            }
            catch (Exception ex)
            {
                var errorInfo = $"Error de conexión:\n" +
                               $"Tipo: {ex.GetType().Name}\n" +
                               $"Mensaje: {ex.Message}\n" +
                               $"Stack Trace:\n{ex.StackTrace}";
                
                if (ex.InnerException != null)
                {
                    errorInfo += $"\n\nInner Exception:\n" +
                                $"Tipo: {ex.InnerException.GetType().Name}\n" +
                                $"Mensaje: {ex.InnerException.Message}";
                }
                
                MostrarResultado(pnlResultConexion, errorInfo, false);
            }
        }

        protected void btnTestConsulta_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    // Probar una consulta muy simple
                    var roles = db.Roles.ToList();
                    var info = $"Consulta exitosa:\n" +
                              $"Roles encontrados: {roles.Count}\n";
                    
                    foreach (var rol in roles.Take(3))
                    {
                        info += $"• {rol.IdRol}: {rol.Nombre}\n";
                    }
                    
                    if (roles.Count > 3)
                        info += $"... y {roles.Count - 3} más\n";

                    MostrarResultado(pnlResultConsulta, info, true);
                }
            }
            catch (Exception ex)
            {
                var errorInfo = $"Error en consulta:\n" +
                               $"Tipo: {ex.GetType().Name}\n" +
                               $"Mensaje: {ex.Message}\n" +
                               $"Stack Trace:\n{ex.StackTrace}";
                
                if (ex.InnerException != null)
                {
                    errorInfo += $"\n\nInner Exception:\n" +
                                $"Tipo: {ex.InnerException.GetType().Name}\n" +
                                $"Mensaje: {ex.InnerException.Message}";
                }
                
                MostrarResultado(pnlResultConsulta, errorInfo, false);
            }
        }

        protected void btnTestInsercion_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    // Verificar si ya existe un rol de prueba
                    var rolExistente = db.Roles.FirstOrDefault(r => r.Nombre == "Rol de Prueba");
                    
                    if (rolExistente != null)
                    {
                        MostrarResultado(pnlResultInsercion, 
                            $"Rol de prueba ya existe:\n" +
                            $"ID: {rolExistente.IdRol}\n" +
                            $"Nombre: {rolExistente.Nombre}", true);
                        return;
                    }

                    // Crear un rol de prueba
                    var nuevoRol = new Rol
                    {
                        Nombre = "Rol de Prueba"
                    };

                    db.Roles.Add(nuevoRol);
                    var result = db.SaveChanges();
                    
                    var info = $"Inserción exitosa:\n" +
                              $"Filas afectadas: {result}\n" +
                              $"ID del nuevo rol: {nuevoRol.IdRol}\n" +
                              $"Nombre: {nuevoRol.Nombre}";

                    MostrarResultado(pnlResultInsercion, info, true);
                }
            }
            catch (Exception ex)
            {
                var errorInfo = $"Error en inserción:\n" +
                               $"Tipo: {ex.GetType().Name}\n" +
                               $"Mensaje: {ex.Message}\n" +
                               $"Stack Trace:\n{ex.StackTrace}";
                
                if (ex.InnerException != null)
                {
                    errorInfo += $"\n\nInner Exception:\n" +
                                $"Tipo: {ex.InnerException.GetType().Name}\n" +
                                $"Mensaje: {ex.InnerException.Message}";
                }
                
                MostrarResultado(pnlResultInsercion, errorInfo, false);
            }
        }

        protected void btnInfoConfig_Click(object sender, EventArgs e)
        {
            try
            {
                var info = $"Información de Configuración:\n\n";
                
                // Información de Entity Framework
                info += $"Entity Framework Version: {typeof(System.Data.Entity.DbContext).Assembly.GetName().Version}\n";
                info += $"MySQL Provider Version: {typeof(MySql.Data.MySqlClient.MySqlConnection).Assembly.GetName().Version}\n\n";
                
                // Información de conexión
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"];
                info += $"Connection String Name: {connectionString.Name}\n";
                info += $"Provider Name: {connectionString.ProviderName}\n";
                info += $"Connection String: {connectionString.ConnectionString}\n\n";
                
                // Información de DbContext
                using (var db = new AppDbContext())
                {
                    info += $"DbContext Database Name: {db.Database.Connection.Database}\n";
                    info += $"DbContext Data Source: {db.Database.Connection.DataSource}\n";
                    info += $"DbContext State: {db.Database.Connection.State}\n";
                }

                MostrarResultado(pnlResultConfig, info, true);
            }
            catch (Exception ex)
            {
                var errorInfo = $"Error al obtener configuración:\n" +
                               $"Tipo: {ex.GetType().Name}\n" +
                               $"Mensaje: {ex.Message}\n" +
                               $"Stack Trace:\n{ex.StackTrace}";
                
                MostrarResultado(pnlResultConfig, errorInfo, false);
            }
        }

        private void MostrarResultado(Panel panel, string mensaje, bool esExito)
        {
            panel.CssClass = esExito ? "result success" : "result error";
            panel.Controls.Clear();
            
            var icono = new HtmlGenericControl("i");
            icono.Attributes["class"] = esExito ? "fas fa-check-circle" : "fas fa-exclamation-triangle";
            icono.Attributes["style"] = "margin-right: 8px;";
            
            panel.Controls.Add(icono);
            panel.Controls.Add(new LiteralControl(mensaje));
            panel.Visible = true;
        }
    }
} 