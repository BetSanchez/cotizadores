using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using pr_proyecto.Data;
using pr_proyecto.Models;
using pr_proyecto.Repository;

namespace pr_proyecto.Repository
{
    public class SucursalRepository : ISucursalRepository
    {
        private readonly AppDbContext _context;
        public SucursalRepository(AppDbContext context) => _context = context;

        public Sucursal GetById(int id) => _context.Sucursales.Find(id);

        public IEnumerable<Sucursal> GetAll() => _context.Sucursales.ToList();

        public void Add(Sucursal entity)
        {
            _context.Sucursales.Add(entity);
            _context.SaveChanges();
        }


        public void Update(Sucursal entity)
        {
            _context.Sucursales.Attach(entity); 
            _context.Entry(entity).State = EntityState.Modified; 
            _context.SaveChanges(); 
        }


        public void Delete(Sucursal entity)
        {
            _context.Sucursales.Remove(entity);
            _context.SaveChanges();
        }
    }
}