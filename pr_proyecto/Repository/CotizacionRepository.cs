// pr_proyecto.Repositories/CotizacionRepository.cs
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
    }
}