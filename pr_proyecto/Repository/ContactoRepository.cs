// pr_proyecto.Repositories/ContactoRepository.cs
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using pr_proyecto.Data;
using pr_proyecto.Models;
using pr_proyecto.Repository;

namespace pr_proyecto.Repository
{
    public class ContactoRepository : IContactoRepository
    {
        private readonly AppDbContext _context;
        public ContactoRepository(AppDbContext context) => _context = context;

        public Contacto GetById(int id) => _context.Contactos.Find(id);

        public IEnumerable<Contacto> GetAll() => _context.Contactos.ToList();

        public void Add(Contacto entity)
        {
            _context.Contactos.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Contacto entity)
        {
            _context.Contactos.Attach(entity); 
            _context.Entry(entity).State = EntityState.Modified; 
            _context.SaveChanges(); 
        }

        public void Delete(Contacto entity)
        {
            _context.Contactos.Remove(entity);
            _context.SaveChanges();
        }
    }
}