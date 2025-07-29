using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using pr_proyecto.Data;
using pr_proyecto.Models;
using pr_proyecto.Repository;

namespace pr_proyecto.Repository
{
    public class RolRepository : IRolRepository
    {
        private readonly AppDbContext _context;
        public RolRepository(AppDbContext context) => _context = context;

        public Rol GetById(int id) => _context.Roles.Find(id);

        public IEnumerable<Rol> GetAll() => _context.Roles.ToList();

        public void Add(Rol entity)
        {
            _context.Roles.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Rol entity)
        {
            _context.Roles.Attach(entity); 
            _context.Entry(entity).State = EntityState.Modified; 
            _context.SaveChanges();
        }

        public void Delete(Rol entity)
        {
            _context.Roles.Remove(entity);
            _context.SaveChanges();
        }
    }
}