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

        public IEnumerable<Rol> ObtenerTodos() => _repo.GetAll();
    }
}