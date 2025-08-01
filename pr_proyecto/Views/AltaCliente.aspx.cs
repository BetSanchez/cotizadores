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
using MySql.Data.MySqlClient;

namespace pr_proyecto
{
    public partial class AltaCliente : System.Web.UI.Page
    {
        private AppDbContext _db;
        private UsuarioService _usuarioService;
        private EmpresaService _empresaService;
        private SucursalService _sucursalService;
        private ContactoService _contactoService;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Inicializar servicios
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
            if (!IsPostBack)
            {
                CargarColonias();
                CargarEmpresas();
                ConfigurarSeccionesCondicionales();
            }
        }

        private void CargarColonias()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("CargarColonias: Iniciando carga de colonias");
                
                ddlColonia.Items.Clear();
                ddlColonia.Items.Add(new ListItem("-- Seleccione colonia --", ""));
                
                var colonias = _db.Colonias.OrderBy(c => c.Nombre).ToList();
                System.Diagnostics.Debug.WriteLine($"CargarColonias: Colonias encontradas: {colonias.Count}");
                
                foreach (var colonia in colonias)
                {
                    ddlColonia.Items.Add(new ListItem($"{colonia.Nombre}, {colonia.Ciudad}", colonia.IdColonia.ToString()));
                    System.Diagnostics.Debug.WriteLine($"CargarColonias: Agregada colonia: {colonia.Nombre}, {colonia.Ciudad} (ID: {colonia.IdColonia})");
                }
                
                System.Diagnostics.Debug.WriteLine("CargarColonias: Carga completada exitosamente");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CargarColonias: Error al cargar colonias: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"CargarColonias: Stack trace: {ex.StackTrace}");
                
                // En caso de error, mostrar mensaje al usuario
                ddlColonia.Items.Clear();
                ddlColonia.Items.Add(new ListItem("-- Error al cargar colonias --", ""));
                
                MostrarError($"Error al cargar colonias: {ex.Message}");
            }
        }

        private void CargarEmpresas(string filtro = "")
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"CargarEmpresas: Iniciando carga de empresas. Filtro: '{filtro}'");
                
                ddlEmpresaExistente.Items.Clear();
                ddlEmpresaExistente.Items.Add(new ListItem("-- Seleccione empresa --", ""));

                var empresas = _db.Empresas.AsQueryable();
                System.Diagnostics.Debug.WriteLine($"CargarEmpresas: Consulta base creada");
                
                if (!string.IsNullOrEmpty(filtro))
                {
                    empresas = empresas.Where(e => e.Nombre.Contains(filtro));
                    System.Diagnostics.Debug.WriteLine($"CargarEmpresas: Filtro aplicado: {filtro}");
                }

                var empresasList = empresas.OrderBy(e => e.Nombre).ToList();
                System.Diagnostics.Debug.WriteLine($"CargarEmpresas: Empresas encontradas: {empresasList.Count}");

                foreach (var empresa in empresasList)
                {
                    ddlEmpresaExistente.Items.Add(new ListItem(empresa.Nombre, empresa.IdEmpresa.ToString()));
                    System.Diagnostics.Debug.WriteLine($"CargarEmpresas: Agregada empresa: {empresa.Nombre} (ID: {empresa.IdEmpresa})");
                }
                
                System.Diagnostics.Debug.WriteLine($"CargarEmpresas: Carga completada exitosamente");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CargarEmpresas: Error al cargar empresas: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"CargarEmpresas: Stack trace: {ex.StackTrace}");
                
                // En caso de error, mostrar mensaje al usuario
                ddlEmpresaExistente.Items.Clear();
                ddlEmpresaExistente.Items.Add(new ListItem("-- Error al cargar empresas --", ""));
                
                MostrarError($"Error al cargar empresas: {ex.Message}");
            }
        }

        private void CargarSucursalesPorEmpresa(int idEmpresa)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"CargarSucursalesPorEmpresa: Cargando sucursales para empresa ID: {idEmpresa}");
                
                ddlSucursalExistente.Items.Clear();
                ddlSucursalExistente.Items.Add(new ListItem("-- Seleccione sucursal --", ""));

                // Usar Entity Framework con configuración específica para evitar problemas de relaciones
                var sucursales = _db.Sucursales
                    .Where(s => s.IdEmpresa == idEmpresa)
                    .Select(s => new { s.IdSucursal, s.NomComercial })
                    .OrderBy(s => s.NomComercial)
                    .ToList();
                
                System.Diagnostics.Debug.WriteLine($"CargarSucursalesPorEmpresa: Sucursales encontradas: {sucursales.Count}");

                foreach (var sucursal in sucursales)
                {
                    ddlSucursalExistente.Items.Add(new ListItem(sucursal.NomComercial, sucursal.IdSucursal.ToString()));
                    System.Diagnostics.Debug.WriteLine($"CargarSucursalesPorEmpresa: Agregada sucursal: {sucursal.NomComercial} (ID: {sucursal.IdSucursal})");
                }
                
                System.Diagnostics.Debug.WriteLine($"CargarSucursalesPorEmpresa: Carga completada exitosamente");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CargarSucursalesPorEmpresa: Error al cargar sucursales: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"CargarSucursalesPorEmpresa: Stack trace: {ex.StackTrace}");
                
                // En caso de error, mostrar mensaje al usuario y limpiar el dropdown
                ddlSucursalExistente.Items.Clear();
                ddlSucursalExistente.Items.Add(new ListItem("-- Error al cargar sucursales --", ""));
                
                // Mostrar error al usuario
                MostrarError($"Error al cargar sucursales: {ex.Message}");
            }
        }

        protected void txtFiltroEmpresa_TextChanged(object sender, EventArgs e)
        {
            CargarEmpresas(txtFiltroEmpresa.Text.Trim());
        }

        protected void ddlEmpresaExistente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlEmpresaExistente.SelectedValue))
            {
                int idEmpresa = int.Parse(ddlEmpresaExistente.SelectedValue);
                CargarSucursalesPorEmpresa(idEmpresa);
                sucursalExistenteSection.Visible = true;
            }
            else
            {
                ddlSucursalExistente.Items.Clear();
                ddlSucursalExistente.Items.Add(new ListItem("-- Primero seleccione una empresa --", ""));
                sucursalExistenteSection.Visible = false;
            }
        }

        private void ConfigurarSeccionesCondicionales()
        {
            empresaSection.Visible = chkNuevaEmpresa.Checked;
            empresaExistenteSection.Visible = !chkNuevaEmpresa.Checked;
            
            // Configurar secciones de sucursal según la selección de empresa
            if (chkNuevaEmpresa.Checked)
            {
                // Nueva empresa requiere nueva sucursal
                chkNuevaSucursal.Checked = true;
                chkNuevaSucursal.Enabled = false;
                sucursalSection.Visible = true;
                sucursalExistenteSection.Visible = false;
            }
            else
            {
                // Empresa existente permite elegir tipo de sucursal
                chkNuevaSucursal.Enabled = true;
                chkNuevaSucursal.Checked = false;
                sucursalSection.Visible = false;
                sucursalExistenteSection.Visible = false; // Se mostrará cuando se seleccione una empresa
            }
        }

        protected void chkNuevaEmpresa_CheckedChanged(object sender, EventArgs e)
        {
            empresaSection.Visible = chkNuevaEmpresa.Checked;
            empresaExistenteSection.Visible = !chkNuevaEmpresa.Checked;
            
            // Si se selecciona nueva empresa, forzosamente debe ser nueva sucursal
            if (chkNuevaEmpresa.Checked)
            {
                chkNuevaSucursal.Checked = true;
                chkNuevaSucursal.Enabled = false; // Deshabilitar para que no se pueda cambiar
                sucursalSection.Visible = true;
                sucursalExistenteSection.Visible = false;
                
                // Limpiar selección de sucursal existente
                ddlSucursalExistente.Items.Clear();
                ddlSucursalExistente.Items.Add(new ListItem("-- Nueva empresa requiere nueva sucursal --", ""));
            }
            else
            {
                // Si se selecciona empresa existente, habilitar opciones de sucursal
                chkNuevaSucursal.Enabled = true;
                chkNuevaSucursal.Checked = false;
                sucursalSection.Visible = false;
                sucursalExistenteSection.Visible = false;
                
                // Limpiar selección de sucursal
                ddlSucursalExistente.Items.Clear();
                ddlSucursalExistente.Items.Add(new ListItem("-- Primero seleccione una empresa --", ""));
            }
        }

        protected void chkNuevaSucursal_CheckedChanged(object sender, EventArgs e)
        {
            sucursalSection.Visible = chkNuevaSucursal.Checked;
            sucursalExistenteSection.Visible = !chkNuevaSucursal.Checked;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();

            if (!Page.IsValid)
            {
                MostrarError("Por favor, corrija los errores de validación antes de continuar.");
                return;
            }

            try
            {
                // Validar secciones condicionales
                if (chkNuevaEmpresa.Checked && string.IsNullOrEmpty(txtNombreEmpresa.Text.Trim()))
                {
                    MostrarError("Debe proporcionar el nombre de la empresa.");
                    return;
                }

                if (!chkNuevaEmpresa.Checked && string.IsNullOrEmpty(ddlEmpresaExistente.SelectedValue))
                {
                    MostrarError("Debe seleccionar una empresa existente.");
                    return;
                }

                // Validar sucursal según el tipo de empresa
                if (chkNuevaEmpresa.Checked)
                {
                    // Nueva empresa requiere nueva sucursal
                    if (string.IsNullOrEmpty(txtNomComercial.Text.Trim()))
                    {
                        MostrarError("Debe proporcionar el nombre comercial de la sucursal.");
                        return;
                    }

                    if (string.IsNullOrEmpty(ddlColonia.SelectedValue))
                    {
                        MostrarError("Debe seleccionar una colonia para la sucursal.");
                        return;
                    }
                }
                else
                {
                    // Empresa existente puede tener sucursal nueva o existente
                    if (chkNuevaSucursal.Checked)
                    {
                        if (string.IsNullOrEmpty(txtNomComercial.Text.Trim()))
                        {
                            MostrarError("Debe proporcionar el nombre comercial de la sucursal.");
                            return;
                        }

                        if (string.IsNullOrEmpty(ddlColonia.SelectedValue))
                        {
                            MostrarError("Debe seleccionar una colonia para la sucursal.");
                            return;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(ddlSucursalExistente.SelectedValue))
                        {
                            MostrarError("Debe seleccionar una sucursal existente.");
                            return;
                        }
                    }
                }

                // Procesar registro
                var resultado = ProcesarRegistroCompleto();

                if (resultado.Exito)
                {
                    MostrarExito($"Cliente '{resultado.NombreCliente}' registrado exitosamente con ID: {resultado.IdCliente}");
                    LimpiarFormulario();
                }
                else
                {
                    MostrarError(resultado.MensajeError);
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error inesperado: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Error al registrar cliente: {ex}");
            }
        }

        private ResultadoRegistro ProcesarRegistroCompleto()
        {
            var resultado = new ResultadoRegistro();

            try
            {
                System.Diagnostics.Debug.WriteLine("ProcesarRegistroCompleto: Iniciando proceso de registro");
                
                // 1. Crear o obtener empresa
                int idEmpresa = 0;
                if (chkNuevaEmpresa.Checked)
                {
                    System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Creando nueva empresa: {txtNombreEmpresa.Text.Trim()}");
                    
                    var empresa = new Empresa
                    {
                        Nombre = txtNombreEmpresa.Text.Trim()
                    };

                    try
                    {
                        _empresaService.Registrar(empresa);
                        idEmpresa = empresa.IdEmpresa;
                        System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Empresa creada con ID: {idEmpresa}");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Error al crear empresa: {ex.Message}");
                        System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Stack trace: {ex.StackTrace}");
                        throw new Exception($"Error al crear empresa: {ex.Message}", ex);
                    }
                }
                else
                {
                    // Usar empresa existente seleccionada
                    idEmpresa = int.Parse(ddlEmpresaExistente.SelectedValue);
                    System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Usando empresa existente con ID: {idEmpresa}");
                }

                // 2. Crear o obtener sucursal
                int idSucursal = 0;
                if (chkNuevaEmpresa.Checked || chkNuevaSucursal.Checked)
                {
                    System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Creando nueva sucursal para empresa ID: {idEmpresa}");
                    
                    var sucursal = new Sucursal
                    {
                        NomComercial = txtNomComercial.Text.Trim(),
                        Telefono = txtTelefonoSucursal.Text.Trim(),
                        Correo = txtCorreoSucursal.Text.Trim(),
                        Direccion = txtDireccionSucursal.Text.Trim(),
                        Metros = !string.IsNullOrEmpty(txtMetros.Text) ? double.Parse(txtMetros.Text) : 0,
                        IdEmpresa = idEmpresa,
                        IdColonia = int.Parse(ddlColonia.SelectedValue)
                    };

                    try
                    {
                        _sucursalService.Registrar(sucursal);
                        idSucursal = sucursal.IdSucursal;
                        System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Sucursal creada con ID: {idSucursal}");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Error al crear sucursal: {ex.Message}");
                        System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Stack trace: {ex.StackTrace}");
                        throw new Exception($"Error al crear sucursal: {ex.Message}", ex);
                    }
                }
                else
                {
                    // Usar sucursal existente seleccionada
                    idSucursal = int.Parse(ddlSucursalExistente.SelectedValue);
                    System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Usando sucursal existente con ID: {idSucursal}");
                }

                // 3. Crear usuario/cliente
                System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Creando usuario: {txtNombre.Text.Trim()} {txtApPaterno.Text.Trim()}");
                
                var usuario = new Usuario
                {
                    Nombre = txtNombre.Text.Trim(),
                    ApPaterno = txtApPaterno.Text.Trim(),
                    ApMaterno = txtApMaterno.Text.Trim(),
                    Correo = txtCorreo.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    NomUsuario = txtCorreo.Text.Trim(), // Usar el correo como nombre de usuario
                    Contrasena = "Cliente" + DateTime.Now.Year, // Contraseña por defecto
                    Estatus = true, // Por defecto activo
                    IdRol = 2, // Rol de cliente por defecto
                    FechaActivacion = DateTime.Now,
                    // Campos obligatorios según la estructura de la base de datos
                    Carrera = "Cliente", // Valor por defecto para clientes
                    Direccion = "Dirección del cliente" // Valor por defecto
                };

                try
                {
                    _usuarioService.Registrar(usuario);
                    System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Usuario creado con ID: {usuario.IdUsuario}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Error al crear usuario: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Stack trace: {ex.StackTrace}");
                    throw new Exception($"Error al crear usuario: {ex.Message}", ex);
                }

                // 4. Crear contacto vinculado solo a la sucursal
                System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Creando contacto para sucursal ID: {idSucursal}");
                
                var contacto = new Contacto
                {
                    Nombre = usuario.Nombre,
                    ApPaterno = usuario.ApPaterno,
                    ApMaterno = usuario.ApMaterno,
                    Correo = usuario.Correo,
                    Telefono = usuario.Telefono,
                    Puesto = txtPuesto.Text.Trim(),
                    IdSucursal = idSucursal // Solo referencia a la sucursal, no a la empresa
                };

                try
                {
                    _contactoService.Registrar(contacto);
                    System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Contacto creado exitosamente");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Error al crear contacto: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Stack trace: {ex.StackTrace}");
                    throw new Exception($"Error al crear contacto: {ex.Message}", ex);
                }

                resultado.Exito = true;
                resultado.IdCliente = usuario.IdUsuario;
                resultado.NombreCliente = $"{usuario.Nombre} {usuario.ApPaterno}";
                resultado.MensajeError = null;
                
                System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Registro completado exitosamente. Cliente ID: {resultado.IdCliente}");

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Error general en el proceso: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"ProcesarRegistroCompleto: Stack trace completo: {ex.StackTrace}");
                
                resultado.Exito = false;
                resultado.MensajeError = ex.Message;
                resultado.IdCliente = 0;
                resultado.NombreCliente = null;
            }

            return resultado;
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
            // Limpiar campos del cliente
            txtNombre.Text = "";
            txtApPaterno.Text = "";
            txtApMaterno.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
            txtPuesto.Text = "";

            // Limpiar campos de empresa
            txtNombreEmpresa.Text = "";
            txtRazonSocial.Text = "";
            txtGiro.Text = "";
            txtFiltroEmpresa.Text = "";

            // Limpiar campos de sucursal
            txtNomComercial.Text = "";
            txtTelefonoSucursal.Text = "";
            txtCorreoSucursal.Text = "";
            txtDireccionSucursal.Text = "";
            txtMetros.Text = "";

            // Resetear dropdowns
            ddlColonia.SelectedIndex = 0;
            ddlEmpresaExistente.SelectedIndex = 0;
            ddlSucursalExistente.Items.Clear();
            ddlSucursalExistente.Items.Add(new ListItem("-- Primero seleccione una empresa --", ""));

            // Resetear checkboxes
            chkNuevaEmpresa.Checked = false;
            chkNuevaSucursal.Checked = false;
            chkNuevaSucursal.Enabled = true; // Habilitar el checkbox de sucursal
            ConfigurarSeccionesCondicionales();
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

    public class ResultadoRegistro
    {
        public bool Exito { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string MensajeError { get; set; }
    }
}
