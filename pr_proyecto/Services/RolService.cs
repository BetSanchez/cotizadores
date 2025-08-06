// pr_proyecto.Services/RolService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using pr_proyecto.Models;
using pr_proyecto.Repository;

namespace pr_proyecto.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _repo;
        public RolService(IRolRepository repo) => _repo = repo;

        public void Registrar(Rol rol)
        {
            if (string.IsNullOrWhiteSpace(rol.Nombre))
                throw new ArgumentException("El nombre del rol es obligatorio.");

            _repo.Add(rol);
        }

        public void Agregar(Rol rol)
        {
            if (string.IsNullOrWhiteSpace(rol.Nombre))
                throw new ArgumentException("El nombre del rol es obligatorio.");

            _repo.Add(rol);
        }

        public IEnumerable<Rol> ObtenerTodos() => _repo.GetAll();

        public Rol ObtenerPorId(int id) => _repo.GetById(id);

        public void Actualizar(Rol rol)
        {
            if (string.IsNullOrWhiteSpace(rol.Nombre))
                throw new ArgumentException("El nombre del rol es obligatorio.");

            _repo.Update(rol);
        }

        public void Eliminar(int id)
        {
            var rol = _repo.GetById(id);
            if (rol == null)
                throw new ArgumentException("El rol no existe.");

            _repo.Delete(rol);
        }

        // Métodos para permisos (implementación mock por ahora)
        public IEnumerable<Privilegio> ObtenerTodosPermisos()
        {
            // Mock de permisos básicos
            return new List<Privilegio>
            {
                new Privilegio { IdPrivilegio = 1, Nombre = "Crear Cotizaciones", Valor = 1 },
                new Privilegio { IdPrivilegio = 2, Nombre = "Editar Cotizaciones", Valor = 2 },
                new Privilegio { IdPrivilegio = 3, Nombre = "Eliminar Cotizaciones", Valor = 4 },
                new Privilegio { IdPrivilegio = 4, Nombre = "Ver Reportes", Valor = 8 },
                new Privilegio { IdPrivilegio = 5, Nombre = "Gestionar Usuarios", Valor = 16 },
                new Privilegio { IdPrivilegio = 6, Nombre = "Gestionar Servicios", Valor = 32 },
                new Privilegio { IdPrivilegio = 7, Nombre = "Gestionar Empresas", Valor = 64 },
                new Privilegio { IdPrivilegio = 8, Nombre = "Acceso Total", Valor = 128 }
            };
        }

        public IEnumerable<Privilegio> ObtenerPermisosPorRol(int idRol)
        {
            // Mock - retornar algunos permisos según el rol
            var todosLosPermisos = ObtenerTodosPermisos();
            
            switch (idRol)
            {
                case 1: // Administrador
                    return todosLosPermisos;
                case 2: // Vendedor
                    return todosLosPermisos.Take(4);
                default:
                    return new List<Privilegio>();
            }
        }

        public void CrearPermiso(Privilegio permiso)
        {
            if (string.IsNullOrWhiteSpace(permiso.Nombre))
                throw new ArgumentException("El nombre del permiso es obligatorio.");

            // Mock - en implementación real se guardaría en BD
            System.Diagnostics.Debug.WriteLine($"Permiso creado: {permiso.Nombre} - Código: {permiso.Valor}");
        }

        public void EliminarPermiso(int idPermiso)
        {
            // Mock - en implementación real se eliminaría de BD
            System.Diagnostics.Debug.WriteLine($"Permiso eliminado: ID {idPermiso}");
        }

        public void AsignarPermisos(int idRol, List<int> idsPermisos)
        {
            // Mock - en implementación real se guardarían las relaciones en BD
            System.Diagnostics.Debug.WriteLine($"Permisos asignados al rol {idRol}: [{string.Join(", ", idsPermisos)}]");
        }
    }
}