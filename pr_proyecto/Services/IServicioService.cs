using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Services
{
    public interface IServicioService
    {
        IEnumerable<Servicio> ObtenerTodos();
        Servicio ObtenerPorId(int id);
        void Agregar(Servicio servicio);
        void Actualizar(Servicio servicio);
        void Eliminar(int id);
    }
}