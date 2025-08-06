using System.Collections.Generic;
using pr_proyecto.Models;
using pr_proyecto.Repository;

namespace pr_proyecto.Services
{
    public class ServicioService : IServicioService
    {
        private readonly IServicioRepository _servicioRepository;

        public ServicioService(IServicioRepository servicioRepository)
        {
            _servicioRepository = servicioRepository;
        }

        public IEnumerable<Servicio> ObtenerTodos()
        {
            return _servicioRepository.ObtenerTodos();
        }

        public Servicio ObtenerPorId(int id)
        {
            return _servicioRepository.ObtenerPorId(id);
        }

        public void Agregar(Servicio servicio)
        {
            _servicioRepository.Agregar(servicio);
        }

        public void Actualizar(Servicio servicio)
        {
            _servicioRepository.Actualizar(servicio);
        }

        public void Eliminar(int id)
        {
            _servicioRepository.Eliminar(id);
        }
    }
}