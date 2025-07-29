using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Services
{
    public interface IContactoService
    {
        void Registrar(Contacto contacto);
        IEnumerable<Contacto> ObtenerTodos();
    }
}