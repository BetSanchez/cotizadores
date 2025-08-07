using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Services
{
    public interface IEmpresaService
    {
        void Registrar(Empresa empresa);
        IEnumerable<Empresa> ObtenerTodas();
        IEnumerable<Empresa> ObtenerTodos();
        Empresa ObtenerPorId(int id);
        void Actualizar(Empresa empresa);
        void Eliminar(int id);
    }
}