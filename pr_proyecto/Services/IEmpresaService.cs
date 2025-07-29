using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Services
{
    public interface IEmpresaService
    {
        void Registrar(Empresa empresa);
        IEnumerable<Empresa> ObtenerTodos();
    }
}