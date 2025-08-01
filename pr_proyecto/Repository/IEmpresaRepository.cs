using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Repository
{
    public interface IEmpresaRepository
    {
        void Add(Empresa empresa);
        void Update(Empresa empresa);
        void Delete(Empresa empresa);
        Empresa GetById(int id);
        IEnumerable<Empresa> GetAll();
    }
}