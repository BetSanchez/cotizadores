using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Services
{
    public interface ISucursalService
    {
        void Registrar(Sucursal sucursal);
        IEnumerable<Sucursal> ObtenerTodas();
        Sucursal ObtenerPorId(int id);
        IEnumerable<Sucursal> ObtenerPorEmpresa(int idEmpresa);
        void Actualizar(Sucursal sucursal);
        void Eliminar(int id);
    }
}