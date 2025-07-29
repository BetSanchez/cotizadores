using System.Collections.Generic;
using System.Linq;
using pr_proyecto.Data;
using pr_proyecto.Models;
using pr_proyecto.Repository;

namespace pr_proyecto.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public Usuario GetById(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _context.Usuarios.ToList();
        }

        public void Add(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void Delete(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
    

        public void Update(Usuario usuario)
        {
            
        }
    }
}