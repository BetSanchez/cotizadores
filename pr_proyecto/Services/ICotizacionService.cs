using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Services
{
    public interface ICotizacionService
    {
        void Registrar(Cotizacion cotizacion);
        IEnumerable<Cotizacion> ObtenerTodos();
        Cotizacion ObtenerUltimaCotizacion();
        Cotizacion ObtenerPorId(int id);
        void Actualizar(Cotizacion cotizacion);
        void Eliminar(Cotizacion cotizacion);
        void CrearCotizacion(Cotizacion cotizacion, List<CotizacionServicio> servicios);
        IEnumerable<Cotizacion> ObtenerUltimasCotizaciones(int cantidad);
    }
}