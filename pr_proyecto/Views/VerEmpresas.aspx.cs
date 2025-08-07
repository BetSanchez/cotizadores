using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using pr_proyecto.Helpers;
using pr_proyecto.Models;
using pr_proyecto.Repository;
using pr_proyecto.Services;

namespace pr_proyecto.Views
{
    public partial class VerEmpresas : System.Web.UI.Page
    {
        private readonly IEmpresaService _empresaService;
        private readonly ISucursalService _sucursalService;
        private List<Empresa> _empresasFiltradas;
        private List<Sucursal> _sucursalesFiltradas;

        public VerEmpresas()
        {
            var empresaRepository = new EmpresaRepository(new Data.AppDbContext());
            var sucursalRepository = new SucursalRepository(new Data.AppDbContext());
            _empresaService = new EmpresaService(empresaRepository);
            _sucursalService = new SucursalService(sucursalRepository);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar autenticación
            AuthHelper.RequireAuthentication();

            if (!IsPostBack)
            {
                CargarEmpresas();
                CargarSucursales();
                CargarDropDownEmpresas();
            }
        }

        private void CargarEmpresas()
        {
            try
            {
                var todasLasEmpresas = _empresaService.ObtenerTodos().ToList();
                _empresasFiltradas = AplicarFiltrosEmpresas(todasLasEmpresas);
                
                gvEmpresas.DataSource = _empresasFiltradas;
                gvEmpresas.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al cargar empresas: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void CargarSucursales()
        {
            try
            {
                var todasLasSucursales = _sucursalService.ObtenerTodos().ToList();
                _sucursalesFiltradas = todasLasSucursales;
                
                gvSucursales.DataSource = _sucursalesFiltradas;
                gvSucursales.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al cargar sucursales: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void CargarDropDownEmpresas()
        {
            try
            {
                var empresas = _empresaService.ObtenerTodos().ToList();
                ddlEmpresaSucursal.Items.Clear();
                ddlEmpresaSucursal.Items.Add(new ListItem("Seleccione una empresa", ""));
                
                foreach (var empresa in empresas)
                {
                    ddlEmpresaSucursal.Items.Add(new ListItem(empresa.Nombre, empresa.IdEmpresa.ToString()));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cargando dropdown empresas: {ex.Message}");
            }
        }

        private List<Empresa> AplicarFiltrosEmpresas(List<Empresa> empresas)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarEmpresa.Text))
                return empresas;

            string termino = txtBuscarEmpresa.Text.Trim().ToLower();
            return empresas.Where(e => 
                (e.Nombre?.ToLower().Contains(termino) ?? false)
            ).ToList();
        }

        protected void btnBuscarEmpresa_Click(object sender, EventArgs e)
        {
            CargarEmpresas();
        }

        protected void btnLimpiarEmpresa_Click(object sender, EventArgs e)
        {
            txtBuscarEmpresa.Text = "";
            CargarEmpresas();
        }

        protected void gvEmpresas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmpresas.PageIndex = e.NewPageIndex;
            CargarEmpresas();
        }

        protected void gvSucursales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSucursales.PageIndex = e.NewPageIndex;
            CargarSucursales();
        }

        protected void btnAccionOculto_Click(object sender, EventArgs e)
        {
            try
            {
                string accion = hdnAccion.Value;
                
                switch (accion)
                {
                    case "eliminar_empresa":
                        EliminarEmpresa();
                        break;
                    case "cargar_editar_empresa":
                        CargarEmpresaParaEditar();
                        break;
                    case "eliminar_sucursal":
                        EliminarSucursal();
                        break;
                    case "cargar_editar_sucursal":
                        CargarSucursalParaEditar();
                        break;
                }

                // Limpiar campos ocultos
                hdnAccion.Value = "";
                hdnIdEmpresa.Value = "";
                hdnIdSucursal.Value = "";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void EliminarEmpresa()
        {
            if (string.IsNullOrEmpty(hdnIdEmpresa.Value))
                return;

            int idEmpresa = Convert.ToInt32(hdnIdEmpresa.Value);
            var empresa = _empresaService.ObtenerPorId(idEmpresa);
            
            if (empresa != null)
            {
                _empresaService.Eliminar(idEmpresa);
                CargarEmpresas();
                CargarDropDownEmpresas();
                
                ScriptManager.RegisterStartupScript(this, GetType(), "success",
                    $"alert('Empresa \"{EscapeForJavaScript(empresa.Nombre)}\" eliminada correctamente.');", true);
            }
        }

        private void CargarEmpresaParaEditar()
        {
            if (string.IsNullOrEmpty(hdnIdEmpresa.Value))
                return;

            int idEmpresa = Convert.ToInt32(hdnIdEmpresa.Value);
            var empresa = _empresaService.ObtenerPorId(idEmpresa);
            
            if (empresa != null)
            {
                txtNombreEmpresa.Text = empresa.Nombre;
                
                hdnAccion.Value = "editar_empresa";
                
                string script = @"
                    document.querySelector('#modalEmpresaLabel').textContent = 'Editar Empresa';
                    var modal = new bootstrap.Modal(document.getElementById('modalNuevaEmpresa'));
                    modal.show();
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "showEditModal", script, true);
            }
        }

        private void EliminarSucursal()
        {
            if (string.IsNullOrEmpty(hdnIdSucursal.Value))
                return;

            int idSucursal = Convert.ToInt32(hdnIdSucursal.Value);
            var sucursal = _sucursalService.ObtenerPorId(idSucursal);
            
            if (sucursal != null)
            {
                _sucursalService.Eliminar(idSucursal);
                CargarSucursales();
                
                ScriptManager.RegisterStartupScript(this, GetType(), "success",
                    $"alert('Sucursal \"{EscapeForJavaScript(sucursal.NomComercial)}\" eliminada correctamente.');", true);
            }
        }

        private void CargarSucursalParaEditar()
        {
            if (string.IsNullOrEmpty(hdnIdSucursal.Value))
                return;

            int idSucursal = Convert.ToInt32(hdnIdSucursal.Value);
            var sucursal = _sucursalService.ObtenerPorId(idSucursal);
            
            if (sucursal != null)
            {
                ddlEmpresaSucursal.SelectedValue = sucursal.IdEmpresa.ToString();
                txtNomComercial.Text = sucursal.NomComercial;
                txtTelefonoSucursal.Text = sucursal.Telefono ?? "";
                txtCorreoSucursal.Text = sucursal.Correo ?? "";
                txtDireccionSucursal.Text = sucursal.Direccion ?? "";
                
                hdnAccion.Value = "editar_sucursal";
                
                string script = @"
                    document.querySelector('#modalSucursalLabel').textContent = 'Editar Sucursal';
                    var modal = new bootstrap.Modal(document.getElementById('modalNuevaSucursal'));
                    modal.show();
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "showEditModal", script, true);
            }
        }

        protected void btnGuardarEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreEmpresa.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error",
                        "alert('El nombre de la empresa es requerido.');", true);
                    return;
                }

                bool esEdicion = hdnAccion.Value == "editar_empresa" && !string.IsNullOrEmpty(hdnIdEmpresa.Value);
                
                Empresa empresa;
                if (esEdicion)
                {
                    int idEmpresa = Convert.ToInt32(hdnIdEmpresa.Value);
                    empresa = _empresaService.ObtenerPorId(idEmpresa);
                    if (empresa == null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "error",
                            "alert('Empresa no encontrada.');", true);
                        return;
                    }
                }
                else
                {
                    empresa = new Empresa();
                }

                empresa.Nombre = txtNombreEmpresa.Text.Trim();

                if (esEdicion)
                {
                    _empresaService.Actualizar(empresa);
                }
                else
                {
                    _empresaService.Registrar(empresa);
                }

                LimpiarFormularioEmpresa();
                CargarEmpresas();
                CargarDropDownEmpresas();

                string action = esEdicion ? "actualizada" : "creada";
                string script = $@"
                    var modal = bootstrap.Modal.getInstance(document.getElementById('modalNuevaEmpresa'));
                    if (modal) modal.hide();
                    alert('Empresa {action} correctamente.');
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "success", script, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al guardar empresa: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        protected void btnGuardarSucursal_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNomComercial.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error",
                        "alert('El nombre comercial es requerido.');", true);
                    return;
                }

                if (string.IsNullOrEmpty(ddlEmpresaSucursal.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error",
                        "alert('Debe seleccionar una empresa.');", true);
                    return;
                }

                bool esEdicion = hdnAccion.Value == "editar_sucursal" && !string.IsNullOrEmpty(hdnIdSucursal.Value);
                
                Sucursal sucursal;
                if (esEdicion)
                {
                    int idSucursal = Convert.ToInt32(hdnIdSucursal.Value);
                    sucursal = _sucursalService.ObtenerPorId(idSucursal);
                    if (sucursal == null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "error",
                            "alert('Sucursal no encontrada.');", true);
                        return;
                    }
                }
                else
                {
                    sucursal = new Sucursal();
                }

                sucursal.IdEmpresa = Convert.ToInt32(ddlEmpresaSucursal.SelectedValue);
                sucursal.NomComercial = txtNomComercial.Text.Trim();
                sucursal.Telefono = txtTelefonoSucursal.Text.Trim();
                sucursal.Correo = txtCorreoSucursal.Text.Trim();
                sucursal.Direccion = txtDireccionSucursal.Text.Trim();
                sucursal.IdColonia = 1; // Por defecto, necesitaríamos una interfaz para seleccionar colonia

                if (esEdicion)
                {
                    _sucursalService.Actualizar(sucursal);
                }
                else
                {
                    _sucursalService.Registrar(sucursal);
                }

                LimpiarFormularioSucursal();
                CargarSucursales();

                string action = esEdicion ? "actualizada" : "creada";
                string script = $@"
                    var modal = bootstrap.Modal.getInstance(document.getElementById('modalNuevaSucursal'));
                    if (modal) modal.hide();
                    alert('Sucursal {action} correctamente.');
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "success", script, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al guardar sucursal: {EscapeForJavaScript(ex.Message)}');", true);
            }
        }

        private void LimpiarFormularioEmpresa()
        {
            txtNombreEmpresa.Text = "";
            hdnAccion.Value = "";
            hdnIdEmpresa.Value = "";
        }

        private void LimpiarFormularioSucursal()
        {
            ddlEmpresaSucursal.SelectedIndex = 0;
            txtNomComercial.Text = "";
            txtTelefonoSucursal.Text = "";
            txtCorreoSucursal.Text = "";
            txtDireccionSucursal.Text = "";
            hdnAccion.Value = "";
            hdnIdSucursal.Value = "";
        }

        // Métodos helper para el GridView
        protected string GetCantidadSucursales(object idEmpresa)
        {
            if (idEmpresa == null) return "0";
            
            try
            {
                int empresaId = Convert.ToInt32(idEmpresa);
                var sucursales = _sucursalService.ObtenerTodos()
                    .Where(s => s.IdEmpresa == empresaId)
                    .Count();
                return sucursales.ToString();
            }
            catch
            {
                return "0";
            }
        }

        protected string GetBotonesEmpresa(object idEmpresa)
        {
            if (idEmpresa == null) return "";
            
            return $@"
                <button type='button' class='btn btn-primary btn-sm me-1' onclick='editarEmpresa({idEmpresa})'>
                    <i class='fas fa-edit'></i> Editar
                </button>
                <button type='button' class='btn btn-danger btn-sm' onclick='eliminarEmpresa({idEmpresa}, ""Empresa"")'>
                    <i class='fas fa-trash'></i> Eliminar
                </button>
            ";
        }

        protected string GetBotonesSucursal(object idSucursal)
        {
            if (idSucursal == null) return "";
            
            return $@"
                <button type='button' class='btn btn-primary btn-sm me-1' onclick='editarSucursal({idSucursal})'>
                    <i class='fas fa-edit'></i> Editar
                </button>
                <button type='button' class='btn btn-danger btn-sm' onclick='eliminarSucursal({idSucursal}, ""Sucursal"")'>
                    <i class='fas fa-trash'></i> Eliminar
                </button>
            ";
        }

        private string EscapeForJavaScript(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            return text.Replace("'", "\\'")
                      .Replace("\"", "\\\"")
                      .Replace("\n", "\\n")
                      .Replace("\r", "\\r");
        }
    }
}