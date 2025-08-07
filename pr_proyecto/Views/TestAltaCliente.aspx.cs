using System;
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
    public partial class TestAltaCliente : System.Web.UI.Page
    {
        private AppDbContext _db;
        private UsuarioService _usuarioService;
        private EmpresaService _empresaService;
        private SucursalService _sucursalService;
        private ContactoService _contactoService;

        protected void Page_Init(object sender, EventArgs e)
        {
            _db = new AppDbContext();
            var usuarioRepo = new UsuarioRepository(_db);
            var empresaRepo = new EmpresaRepository(_db);
            var sucursalRepo = new SucursalRepository(_db);
            var contactoRepo = new ContactoRepository(_db);

            _usuarioService = new UsuarioService(usuarioRepo);
            _empresaService = new EmpresaService(empresaRepo);
            _sucursalService = new SucursalService(sucursalRepo);
            _contactoService = new ContactoService(contactoRepo);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Página de carga
        }

        protected void btnTestConexion_Click(object sender, EventArgs e)
        {
            try
            {
                var empresas = _db.Empresas.Count();
                var sucursales = _db.Sucursales.Count();
                var contactos = _db.Contactos.Count();
                var usuarios = _db.Usuarios.Count();

                MostrarResultado(pnlResultConexion, $"Conexión exitosa. Datos en BD: {empresas} empresas, {sucursales} sucursales, {contactos} contactos, {usuarios} usuarios", true);
            }
            catch (Exception ex)
            {
                MostrarResultado(pnlResultConexion, $"Error de conexión: {ex.Message}", false);
            }
        }

        protected void btnTestEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombreEmpresa.Text.Trim()))
                {
                    MostrarResultado(pnlResultEmpresa, "Debe ingresar un nombre de empresa", false);
                    return;
                }

                var empresa = new Empresa
                {
                    Nombre = txtNombreEmpresa.Text.Trim()
                };

                _empresaService.Registrar(empresa);
                MostrarResultado(pnlResultEmpresa, $"Empresa '{empresa.Nombre}' registrada exitosamente con ID: {empresa.IdEmpresa}", true);
                txtNombreEmpresa.Text = "";
            }
            catch (Exception ex)
            {
                MostrarResultado(pnlResultEmpresa, $"Error al registrar empresa: {ex.Message}", false);
            }
        }

        protected void btnTestSucursal_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombreSucursal.Text.Trim()) ||
                    string.IsNullOrEmpty(txtTelefonoSucursal.Text.Trim()) ||
                    string.IsNullOrEmpty(txtCorreoSucursal.Text.Trim()))
                {
                    MostrarResultado(pnlResultSucursal, "Debe completar todos los campos obligatorios", false);
                    return;
                }

                // Obtener la primera empresa disponible
                var empresa = _db.Empresas.FirstOrDefault();
                if (empresa == null)
                {
                    MostrarResultado(pnlResultSucursal, "No hay empresas registradas. Registre una empresa primero.", false);
                    return;
                }

                // Obtener la primera colonia disponible
                var colonia = _db.Colonias.FirstOrDefault();
                if (colonia == null)
                {
                    MostrarResultado(pnlResultSucursal, "No hay colonias registradas en la base de datos.", false);
                    return;
                }

                var sucursal = new Sucursal
                {
                    NomComercial = txtNombreSucursal.Text.Trim(),
                    Telefono = txtTelefonoSucursal.Text.Trim(),
                    Correo = txtCorreoSucursal.Text.Trim(),
                    Direccion = "Dirección de prueba",
                    IdEmpresa = empresa.IdEmpresa,
                    IdColonia = colonia.IdColonia
                };

                _sucursalService.Registrar(sucursal);
                MostrarResultado(pnlResultSucursal, $"Sucursal '{sucursal.NomComercial}' registrada exitosamente con ID: {sucursal.IdSucursal}", true);
                
                // Limpiar campos
                txtNombreSucursal.Text = "";
                txtTelefonoSucursal.Text = "";
                txtCorreoSucursal.Text = "";
            }
            catch (Exception ex)
            {
                MostrarResultado(pnlResultSucursal, $"Error al registrar sucursal: {ex.Message}", false);
            }
        }

        protected void btnTestClienteCompleto_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Crear empresa de prueba
                var empresa = new Empresa
                {
                    Nombre = $"Empresa Test {DateTime.Now:yyyyMMddHHmmss}"
                };
                _empresaService.Registrar(empresa);

                // 2. Crear sucursal de prueba
                var colonia = _db.Colonias.FirstOrDefault();
                if (colonia == null)
                {
                    MostrarResultado(pnlResultCliente, "No hay colonias registradas en la base de datos.", false);
                    return;
                }

                var sucursal = new Sucursal
                {
                    NomComercial = $"Sucursal Test {DateTime.Now:yyyyMMddHHmmss}",
                    Telefono = "1234567890",
                    Correo = "test@sucursal.com",
                    Direccion = "Dirección de prueba",
                    IdEmpresa = empresa.IdEmpresa,
                    IdColonia = colonia.IdColonia
                };
                _sucursalService.Registrar(sucursal);

                // 3. Crear usuario/cliente
                var rol = _db.Roles.FirstOrDefault();
                if (rol == null)
                {
                    MostrarResultado(pnlResultCliente, "No hay roles registrados en la base de datos.", false);
                    return;
                }

                var usuario = new Usuario
                {
                    Nombre = "Cliente",
                    ApPaterno = "Test",
                    ApMaterno = "Completo",
                    Correo = $"cliente.test{DateTime.Now:yyyyMMddHHmmss}@test.com",
                    Telefono = "0987654321",
                    NomUsuario = $"cliente_test_{DateTime.Now:yyyyMMddHHmmss}",
                    Contraseña = "123456",
                    Estatus = true,
                    IdRol = rol.IdRol,
                    FechaActivacion = DateTime.Now
                };
                _usuarioService.Registrar(usuario);

                // 4. Crear contacto
                var contacto = new Contacto
                {
                    Nombre = usuario.Nombre,
                    ApPaterno = usuario.ApPaterno,
                    ApMaterno = usuario.ApMaterno,
                    Correo = usuario.Correo,
                    Telefono = usuario.Telefono,
                    Puesto = "Cliente",
                    IdSucursal = sucursal.IdSucursal
                };
                _contactoService.Registrar(contacto);

                MostrarResultado(pnlResultCliente, 
                    $"Cliente completo registrado exitosamente:<br/>" +
                    $"• Empresa: {empresa.Nombre} (ID: {empresa.IdEmpresa})<br/>" +
                    $"• Sucursal: {sucursal.NomComercial} (ID: {sucursal.IdSucursal})<br/>" +
                    $"• Usuario: {usuario.NomUsuario} (ID: {usuario.IdUsuario})<br/>" +
                    $"• Contacto: {contacto.Nombre} {contacto.ApPaterno} (ID: {contacto.IdContacto})", true);
            }
            catch (Exception ex)
            {
                MostrarResultado(pnlResultCliente, $"Error al registrar cliente completo: {ex.Message}", false);
            }
        }

        protected void btnListarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                var empresas = _db.Empresas.ToList();
                var sucursales = _db.Sucursales.ToList();
                var contactos = _db.Contactos.ToList();
                var usuarios = _db.Usuarios.ToList();

                var resultado = $"<strong>Datos en la base de datos:</strong><br/><br/>";
                
                resultado += $"<strong>Empresas ({empresas.Count}):</strong><br/>";
                foreach (var emp in empresas.Take(5))
                {
                    resultado += $"• {emp.Nombre} (ID: {emp.IdEmpresa})<br/>";
                }
                if (empresas.Count > 5) resultado += $"... y {empresas.Count - 5} más<br/>";

                resultado += $"<br/><strong>Sucursales ({sucursales.Count}):</strong><br/>";
                foreach (var suc in sucursales.Take(5))
                {
                    resultado += $"• {suc.NomComercial} (ID: {suc.IdSucursal})<br/>";
                }
                if (sucursales.Count > 5) resultado += $"... y {sucursales.Count - 5} más<br/>";

                resultado += $"<br/><strong>Contactos ({contactos.Count}):</strong><br/>";
                foreach (var con in contactos.Take(5))
                {
                    resultado += $"• {con.Nombre} {con.ApPaterno} (ID: {con.IdContacto})<br/>";
                }
                if (contactos.Count > 5) resultado += $"... y {contactos.Count - 5} más<br/>";

                resultado += $"<br/><strong>Usuarios ({usuarios.Count}):</strong><br/>";
                foreach (var usu in usuarios.Take(5))
                {
                    resultado += $"• {usu.NomUsuario} - {usu.Nombre} {usu.ApPaterno} (ID: {usu.IdUsuario})<br/>";
                }
                if (usuarios.Count > 5) resultado += $"... y {usuarios.Count - 5} más<br/>";

                MostrarResultado(pnlResultListado, resultado, true);
            }
            catch (Exception ex)
            {
                MostrarResultado(pnlResultListado, $"Error al listar datos: {ex.Message}", false);
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

        protected void Page_Unload(object sender, EventArgs e)
        {
            _db?.Dispose();
        }
    }
} 