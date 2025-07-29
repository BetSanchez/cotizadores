using System;
using System.Collections.Generic;
using System.Linq;
using pr_proyecto.Models;
using pr_proyecto.Repository;

namespace pr_proyecto.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        public UsuarioService(IUsuarioRepository repo)
            => _repo = repo;

        public void Registrar(Usuario usuario)
        {
            // 1. Validaciones de negocio
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (_repo.GetAll().Any(u => u.NomUsuario == usuario.NomUsuario))
                throw new ArgumentException("El nombre de usuario ya existe.");

            // 2. Encriptar contraseña (ejemplo muy básico)
            usuario.Contraseña = Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes(usuario.Contraseña));

            // 3. Asignar valores por defecto
            usuario.FechaActivacion = usuario.FechaActivacion == default
                ? DateTime.Now
                : usuario.FechaActivacion;

            // 4. Persistir
            _repo.Add(usuario);
        }

        public IEnumerable<Usuario> ObtenerTodos()
            => _repo.GetAll();
    }
}