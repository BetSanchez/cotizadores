using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Repository
{
    public interface ISucursalRepository
    {
        void Add(Sucursal sucursal);
        void Update(Sucursal sucursal);
        void Delete(Sucursal sucursal);
        Sucursal GetById(int id);
        IEnumerable<Sucursal> GetAll();
    }
}