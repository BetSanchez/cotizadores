using System.Collections.Generic;
using System.Linq;
using pr_proyecto.Models;
using pr_proyecto.Repository;

namespace pr_proyecto.Services
{
    public class CotizacionService : ICotizacionService
    {
        private readonly ICotizacionRepository _cotizacionRepository;
        private readonly ICotizacionServicioRepository _cotizacionServicioRepository;

        public CotizacionService(ICotizacionRepository cotizacionRepository, ICotizacionServicioRepository cotizacionServicioRepository)
        {
            _cotizacionRepository = cotizacionRepository;
            _cotizacionServicioRepository = cotizacionServicioRepository;
        }

        public void Registrar(Cotizacion cotizacion)
        {
            _cotizacionRepository.Crear(cotizacion, new List<CotizacionServicio>());
        }

        public IEnumerable<Cotizacion> ObtenerTodos()
        {
            return _cotizacionRepository.GetAll();
        }

        public Cotizacion ObtenerUltimaCotizacion()
        {
            return _cotizacionRepository.ObtenerUltimasCotizaciones(1).FirstOrDefault();
        }

        public void CrearCotizacion(Cotizacion cotizacion, List<CotizacionServicio> servicios)
        {
            // Validar que la cotización y los servicios no sean nulos
            if (cotizacion == null)
                throw new System.ArgumentNullException(nameof(cotizacion));
            if (servicios == null)
                throw new System.ArgumentNullException(nameof(servicios));

            // Crear la cotización con sus servicios en una sola transacción
            _cotizacionRepository.Crear(cotizacion, servicios);
        }

        public Cotizacion ObtenerPorId(int id)
        {
            return _cotizacionRepository.GetById(id);
        }

        public void Actualizar(Cotizacion cotizacion)
        {
            _cotizacionRepository.Update(cotizacion);
        }

        public void Eliminar(Cotizacion cotizacion)
        {
            _cotizacionRepository.Delete(cotizacion);
        }

        public IEnumerable<Cotizacion> ObtenerUltimasCotizaciones(int cantidad)
        {
            return _cotizacionRepository.ObtenerUltimasCotizaciones(cantidad);
        }
    }
}