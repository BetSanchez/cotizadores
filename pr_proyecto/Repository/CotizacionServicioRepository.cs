using System.Collections.Generic;
using System.Linq;
using pr_proyecto.Data;
using pr_proyecto.Models;

namespace pr_proyecto.Repository
{
    public class CotizacionServicioRepository : ICotizacionServicioRepository
    {
        private readonly AppDbContext _context;

        public CotizacionServicioRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CotizacionServicio> ObtenerTodos()
        {
            return _context.CotizacionServicios.ToList();
        }

        public CotizacionServicio ObtenerPorId(int id)
        {
            return _context.CotizacionServicios.Find(id);
        }

        public IEnumerable<CotizacionServicio> ObtenerPorCotizacion(int idCotizacion)
        {
            return _context.CotizacionServicios
                .Where(cs => cs.IdCotizacion == idCotizacion)
                .ToList();
        }

        public void Agregar(CotizacionServicio cotizacionServicio)
        {
            _context.CotizacionServicios.Add(cotizacionServicio);
            _context.SaveChanges();
        }

        public void Actualizar(CotizacionServicio cotizacionServicio)
        {
            var cotizacionServicioExistente = _context.CotizacionServicios.Find(cotizacionServicio.IdCotServicio);
            if (cotizacionServicioExistente != null)
            {
                _context.Entry(cotizacionServicioExistente).CurrentValues.SetValues(cotizacionServicio);
                _context.SaveChanges();
            }
        }

        public void Eliminar(int id)
        {
            var cotizacionServicio = _context.CotizacionServicios.Find(id);
            if (cotizacionServicio != null)
            {
                _context.CotizacionServicios.Remove(cotizacionServicio);
                _context.SaveChanges();
            }
        }

        public void EliminarPorCotizacion(int idCotizacion)
        {
            var cotizacionServicios = _context.CotizacionServicios
                .Where(cs => cs.IdCotizacion == idCotizacion)
                .ToList();

            foreach (var cotizacionServicio in cotizacionServicios)
            {
                _context.CotizacionServicios.Remove(cotizacionServicio);
            }
            _context.SaveChanges();
        }
    }
} 