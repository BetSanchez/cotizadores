// pr_proyecto.Repository/IRolRepository.cs
using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Repository
{
    public interface IRolRepository
    {
        Rol GetById(int id);
        IEnumerable<Rol> GetAll();
        void Add(Rol entity);
        void Update(Rol entity);
        void Delete(Rol entity);
    }
}