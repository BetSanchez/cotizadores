using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Repository
{
    public interface IUsuarioRepository
    {
        Usuario GetById(int id);
        IEnumerable<Usuario> GetAll();
        void Add(Usuario usuario);
        void Delete(Usuario usuario);
        void Update(Usuario usuario);

    }
}