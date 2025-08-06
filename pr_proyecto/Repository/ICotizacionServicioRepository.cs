using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Repository
{
    public interface ICotizacionServicioRepository
    {
        IEnumerable<CotizacionServicio> ObtenerTodos();
        CotizacionServicio ObtenerPorId(int id);
        IEnumerable<CotizacionServicio> ObtenerPorCotizacion(int idCotizacion);
        void Agregar(CotizacionServicio cotizacionServicio);
        void Actualizar(CotizacionServicio cotizacionServicio);
        void Eliminar(int id);
        void EliminarPorCotizacion(int idCotizacion);
    }
} 