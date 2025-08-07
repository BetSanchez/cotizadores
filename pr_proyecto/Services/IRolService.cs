using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Services
{
    public interface IRolService
    {
        void Registrar(Rol rol);
        void Agregar(Rol rol);
        IEnumerable<Rol> ObtenerTodos();
        Rol ObtenerPorId(int id);
        void Actualizar(Rol rol);
        void Eliminar(int id);
        
        // Métodos para permisos
        IEnumerable<Privilegio> ObtenerTodosPermisos();
        IEnumerable<Privilegio> ObtenerPermisosPorRol(int idRol);
        void CrearPermiso(Privilegio permiso);
        void EliminarPermiso(int idPermiso);
        void AsignarPermisos(int idRol, List<int> idsPermisos);
    }
}