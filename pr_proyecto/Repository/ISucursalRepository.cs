using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Repository
{
    public interface ISucursalRepository
    {
        Sucursal GetById(int id);
        IEnumerable<Sucursal> GetAll();
        void Add(Sucursal entity);
        void Update(Sucursal entity);
        void Delete(Sucursal entity);
    }
}