using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Repository
{
    public interface IEmpresaRepository
    {
        Empresa GetById(int id);
        IEnumerable<Empresa> GetAll();
        void Add(Empresa entity);
        void Update(Empresa entity);
        void Delete(Empresa entity);
    }
}