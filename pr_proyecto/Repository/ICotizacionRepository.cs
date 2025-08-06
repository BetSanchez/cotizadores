using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Repository
{
    public interface ICotizacionRepository
    {
        Cotizacion GetById(int id);
        IEnumerable<Cotizacion> GetAll();
        void Add(Cotizacion entity);
        void Update(Cotizacion entity);
        void Delete(Cotizacion entity);
        IEnumerable<Cotizacion> ObtenerUltimasCotizaciones(int cantidad);
        void Crear(Cotizacion cotizacion, IEnumerable<CotizacionServicio> servicios);
    }
}