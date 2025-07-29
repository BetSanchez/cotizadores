using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Services
{
    public interface IRolService
    {
        void Registrar(Rol rol);
        IEnumerable<Rol> ObtenerTodos();
    }
}