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
    public partial class VerCotizaciones : System.Web.UI.Page
    {
        private readonly ICotizacionService _cotizacionService;
        private List<Cotizacion> _cotizacionesFiltradas;

        public VerCotizaciones()
        {
            var cotizacionRepository = new CotizacionRepository(new Data.AppDbContext());
            var cotizacionServicioRepository = new CotizacionServicioRepository(new Data.AppDbContext());
            _cotizacionService = new CotizacionService(cotizacionRepository, cotizacionServicioRepository);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar autenticación y rol
            AuthHelper.RequireAuthentication();

            if (!IsPostBack)
            {
                CargarCotizaciones();
                CalcularResumen();
            }
        }

        private void CargarCotizaciones()
        {
            try
            {
                var usuarioId = AuthHelper.GetCurrentUserId();
                if (!usuarioId.HasValue)
                {
                    Response.Redirect("~/Views/Login.aspx");
                    return;
                }

                // Obtener todas las cotizaciones del usuario
                var todasLasCotizaciones = _cotizacionService.ObtenerTodos()
                    .Where(c => c.IdUsuarioVendedor == usuarioId.Value)
                    .OrderByDescending(c => c.FechaEmision)
                    .ToList();

                // Aplicar filtros
                _cotizacionesFiltradas = AplicarFiltros(todasLasCotizaciones);

                // Bind al GridView
                gvCotizaciones.DataSource = _cotizacionesFiltradas;
                gvCotizaciones.DataBind();

                // Actualizar resumen
                CalcularResumen();
            }
            catch (Exception ex)
            {
                // Log error y mostrar mensaje
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al cargar cotizaciones: {ex.Message}');", true);
            }
        }

        private List<Cotizacion> AplicarFiltros(List<Cotizacion> cotizaciones)
        {
            var filtradas = cotizaciones.AsQueryable();

            // Filtro por estado
            if (!string.IsNullOrEmpty(ddlEstatus.SelectedValue))
            {
                string estatus = ddlEstatus.SelectedValue;
                filtradas = filtradas.Where(c => c.Estatus == estatus);
            }

            // Filtro por fecha desde
            if (!string.IsNullOrEmpty(txtFechaDesde.Text))
            {
                DateTime fechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
                filtradas = filtradas.Where(c => c.FechaEmision >= fechaDesde);
            }

            // Filtro por fecha hasta
            if (!string.IsNullOrEmpty(txtFechaHasta.Text))
            {
                DateTime fechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
                filtradas = filtradas.Where(c => c.FechaEmision <= fechaHasta);
            }

            return filtradas.ToList();
        }

        private void CalcularResumen()
        {
            try
            {
                var usuarioId = AuthHelper.GetCurrentUserId();
                if (!usuarioId.HasValue) return;

                var todasLasCotizaciones = _cotizacionService.ObtenerTodos()
                    .Where(c => c.IdUsuarioVendedor == usuarioId.Value)
                    .ToList();

                // Total cotizaciones
                lblTotalCotizaciones.Text = todasLasCotizaciones.Count.ToString();

                // Monto total
                double montoTotal = todasLasCotizaciones.Sum(c => c.Total);
                lblMontoTotal.Text = montoTotal.ToString("C");

                // Activas
                int activas = todasLasCotizaciones.Count(c => c.Estatus == "A");
                lblActivas.Text = activas.ToString();

                // Este mes
                var inicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                int esteMes = todasLasCotizaciones.Count(c => c.FechaEmision >= inicioMes);
                lblEsteMes.Text = esteMes.ToString();
            }
            catch (Exception ex)
            {
                // Log error silencioso para no interrumpir la carga de la página
                System.Diagnostics.Debug.WriteLine($"Error calculando resumen: {ex.Message}");
            }
        }

        protected void ddlEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCotizaciones();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarCotizaciones();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlEstatus.SelectedIndex = 0;
            txtFechaDesde.Text = "";
            txtFechaHasta.Text = "";
            CargarCotizaciones();
        }

        protected void gvCotizaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCotizaciones.PageIndex = e.NewPageIndex;
            CargarCotizaciones();
        }

        // Métodos helper para el GridView
        protected string GetEstadoBadgeClass(object estatus)
        {
            if (estatus == null) return "secondary";

            string estado = estatus.ToString();
            switch (estado)
            {
                case "A": return "success";
                case "C": return "danger";
                case "P": return "warning";
                default: return "secondary";
            }
        }

        protected string GetEstadoTexto(object estatus)
        {
            if (estatus == null) return "Desconocido";

            string estado = estatus.ToString();
            switch (estado)
            {
                case "A": return "Activa";
                case "C": return "Cancelada";
                case "P": return "Pendiente";
                default: return "Desconocido";
            }
        }
    }
}