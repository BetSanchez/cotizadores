using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using pr_proyecto.Data;
using pr_proyecto.Models;
using pr_proyecto.Repository;

namespace pr_proyecto.Repository
{
    public class CotizacionRepository : ICotizacionRepository
    {
        private readonly AppDbContext _context;
        public CotizacionRepository(AppDbContext context) => _context = context;

        public Cotizacion GetById(int id) => _context.Cotizaciones.Find(id);

        public IEnumerable<Cotizacion> GetAll() => _context.Cotizaciones.ToList();

        public void Add(Cotizacion entity)
        {
            _context.Cotizaciones.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Cotizacion entity)
        {
            _context.Cotizaciones.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Cotizacion entity)
        {
            _context.Cotizaciones.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Cotizacion> ObtenerUltimasCotizaciones(int cantidad)
        {
            return _context.Cotizaciones
                .OrderByDescending(c => c.FechaGuardado)
                .Take(cantidad)
                .ToList();
        }

        public void Crear(Cotizacion cotizacion, IEnumerable<CotizacionServicio> servicios)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Guardar la cotización
                    _context.Cotizaciones.Add(cotizacion);
                    _context.SaveChanges();

                    // Crear nuevos objetos CotizacionServicio para evitar problemas de tracking
                    foreach (var servicio in servicios)
                    {
                        var nuevoCotizacionServicio = new CotizacionServicio
                        {
                            IdCotizacion = cotizacion.IdCotizacion,
                            IdServicio = servicio.IdServicio,
                            Cantidad = servicio.Cantidad,
                            ValorUnitario = servicio.ValorUnitario,
                            Subtotal = servicio.Subtotal,
                            Descripcion = servicio.Descripcion,
                            Terminos = servicio.Terminos,
                            Incluye = servicio.Incluye,
                            Condiciones = servicio.Condiciones
                            // NO asignar Servicio ni Cotizacion para evitar conflictos de tracking
                        };
                        _context.CotizacionServicios.Add(nuevoCotizacionServicio);
                    }

                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}