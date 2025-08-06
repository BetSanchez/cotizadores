using System.Collections.Generic;
using System.Linq;
using pr_proyecto.Data;
using pr_proyecto.Models;

namespace pr_proyecto.Repository
{
    public class ServicioRepository : IServicioRepository
    {
        private readonly AppDbContext _context;

        public ServicioRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Servicio> ObtenerTodos()
        {
            return _context.Servicios.ToList();
        }

        public Servicio ObtenerPorId(int id)
        {
            return _context.Servicios.Find(id);
        }

        public void Agregar(Servicio servicio)
        {
            _context.Servicios.Add(servicio);
            _context.SaveChanges();
        }

        public void Actualizar(Servicio servicio)
        {
            var servicioExistente = _context.Servicios.Find(servicio.IdServicio);
            if (servicioExistente != null)
            {
                _context.Entry(servicioExistente).CurrentValues.SetValues(servicio);
                _context.SaveChanges();
            }
        }

        public void Eliminar(int id)
        {
            var servicio = _context.Servicios.Find(id);
            if (servicio != null)
            {
                _context.Servicios.Remove(servicio);
                _context.SaveChanges();
            }
        }
    }
}