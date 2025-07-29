using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using pr_proyecto.Data;
using pr_proyecto.Models;
using pr_proyecto.Repository;

namespace pr_proyecto.Repository
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly AppDbContext _context;
        public EmpresaRepository(AppDbContext context) => _context = context;

        public Empresa GetById(int id) => _context.Empresas.Find(id);

        public IEnumerable<Empresa> GetAll() => _context.Empresas.ToList();

        public void Add(Empresa entity)
        {
            _context.Empresas.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Empresa entity)
        {
            _context.Set<Empresa>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Empresa entity)
        {
            _context.Empresas.Remove(entity);
            _context.SaveChanges();
        }
    }
}