using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Services
{
    public interface ISucursalService
    {
        void Registrar(Sucursal sucursal);
        IEnumerable<Sucursal> ObtenerTodos();
    }
}