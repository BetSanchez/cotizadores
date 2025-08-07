using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Services
{
    public interface ICotizacionServicioService
    {
        IEnumerable<CotizacionServicio> ObtenerTodos();
        CotizacionServicio ObtenerPorId(int id);
        IEnumerable<CotizacionServicio> ObtenerPorCotizacion(int idCotizacion);
        void Agregar(CotizacionServicio cotizacionServicio);
        void Actualizar(CotizacionServicio cotizacionServicio);
        void Eliminar(int id);
        void EliminarPorCotizacion(int idCotizacion);
        void AgregarServiciosACotizacion(int idCotizacion, List<CotizacionServicio> servicios);
    }
} 