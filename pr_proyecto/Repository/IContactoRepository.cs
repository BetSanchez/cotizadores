// pr_proyecto.Repository/IContactoRepository.cs
using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Repository
{
    public interface IContactoRepository
    {
        Contacto GetById(int id);
        IEnumerable<Contacto> GetAll();
        void Add(Contacto entity);
        void Update(Contacto entity);
        void Delete(Contacto entity);
    }
}