using System.Collections.Generic;
using pr_proyecto.Models;
using pr_proyecto.Repository;

namespace pr_proyecto.Services
{
    public class CotizacionServicioService : ICotizacionServicioService
    {
        private readonly ICotizacionServicioRepository _cotizacionServicioRepository;

        public CotizacionServicioService(ICotizacionServicioRepository cotizacionServicioRepository)
        {
            _cotizacionServicioRepository = cotizacionServicioRepository;
        }

        public IEnumerable<CotizacionServicio> ObtenerTodos()
        {
            return _cotizacionServicioRepository.ObtenerTodos();
        }

        public CotizacionServicio ObtenerPorId(int id)
        {
            return _cotizacionServicioRepository.ObtenerPorId(id);
        }

        public IEnumerable<CotizacionServicio> ObtenerPorCotizacion(int idCotizacion)
        {
            return _cotizacionServicioRepository.ObtenerPorCotizacion(idCotizacion);
        }

        public void Agregar(CotizacionServicio cotizacionServicio)
        {
            _cotizacionServicioRepository.Agregar(cotizacionServicio);
        }

        public void Actualizar(CotizacionServicio cotizacionServicio)
        {
            _cotizacionServicioRepository.Actualizar(cotizacionServicio);
        }

        public void Eliminar(int id)
        {
            _cotizacionServicioRepository.Eliminar(id);
        }

        public void EliminarPorCotizacion(int idCotizacion)
        {
            _cotizacionServicioRepository.EliminarPorCotizacion(idCotizacion);
        }

        public void AgregarServiciosACotizacion(int idCotizacion, List<CotizacionServicio> servicios)
        {
            foreach (var servicio in servicios)
            {
                servicio.IdCotizacion = idCotizacion;
                _cotizacionServicioRepository.Agregar(servicio);
            }
        }
    }
} 