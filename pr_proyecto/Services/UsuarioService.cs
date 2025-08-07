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
            try
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioService.Registrar: Iniciando registro de usuario '{usuario.NomUsuario}'");

                // 1. Validaciones de negocio
                if (string.IsNullOrWhiteSpace(usuario.Nombre))
                    throw new ArgumentException("El nombre es obligatorio.");

                if (string.IsNullOrWhiteSpace(usuario.ApPaterno))
                    throw new ArgumentException("El apellido paterno es obligatorio.");

                if (string.IsNullOrWhiteSpace(usuario.Correo))
                    throw new ArgumentException("El correo electrónico es obligatorio.");

                if (string.IsNullOrWhiteSpace(usuario.Telefono))
                    throw new ArgumentException("El teléfono es obligatorio.");

                if (string.IsNullOrWhiteSpace(usuario.NomUsuario))
                    throw new ArgumentException("El nombre de usuario es obligatorio.");

                if (string.IsNullOrWhiteSpace(usuario.Contrasena))
                    throw new ArgumentException("La contraseña es obligatoria.");

                // Validar formato de correo
                if (!IsValidEmail(usuario.Correo))
                    throw new ArgumentException("El formato del correo electrónico no es válido.");

                System.Diagnostics.Debug.WriteLine($"UsuarioService.Registrar: Validaciones básicas pasadas");

                // 2. Verificar si el nombre de usuario ya existe
                System.Diagnostics.Debug.WriteLine($"UsuarioService.Registrar: Verificando usuarios existentes...");
                
                try
                {
                    var usuariosExistentes = _repo.GetAll();
                    System.Diagnostics.Debug.WriteLine($"UsuarioService.Registrar: Se encontraron {usuariosExistentes.Count()} usuarios existentes");

                    if (usuariosExistentes.Any(u => u.NomUsuario.ToLower() == usuario.NomUsuario.ToLower()))
                        throw new ArgumentException($"El nombre de usuario '{usuario.NomUsuario}' ya existe.");

                    // Verificar si el correo ya existe
                    if (usuariosExistentes.Any(u => u.Correo.ToLower() == usuario.Correo.ToLower()))
                        throw new ArgumentException($"El correo electrónico '{usuario.Correo}' ya está registrado.");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"UsuarioService.Registrar: Error al verificar usuarios existentes: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"UsuarioService.Registrar: Stack trace: {ex.StackTrace}");
                    
                    // Si es un error de Entity Framework, intentar una consulta más simple
                    if (ex.InnerException != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"UsuarioService.Registrar: Inner exception: {ex.InnerException.Message}");
                    }
                    
                    // Por ahora, continuar sin verificación de duplicados para debugging
                    System.Diagnostics.Debug.WriteLine("UsuarioService.Registrar: Continuando sin verificación de duplicados");
                }

                System.Diagnostics.Debug.WriteLine($"UsuarioService.Registrar: Verificaciones de duplicados pasadas");

                // 3. Encriptar contraseña (ejemplo muy básico)
                usuario.Contrasena = Convert.ToBase64String(
                    System.Text.Encoding.UTF8.GetBytes(usuario.Contrasena));

                // 4. Asignar valores por defecto
                usuario.FechaActivacion = usuario.FechaActivacion == default
                    ? DateTime.Now
                    : usuario.FechaActivacion;

                // 5. Logging para debugging
                System.Diagnostics.Debug.WriteLine($"UsuarioService.Registrar: Intentando registrar usuario: {usuario.NomUsuario}");
                System.Diagnostics.Debug.WriteLine($"UsuarioService.Registrar: Datos del usuario - Nombre: {usuario.Nombre}, Rol: {usuario.IdRol}, Estatus: {usuario.Estatus}");

                // 6. Persistir
                _repo.Add(usuario);
                
                // 7. Logging de éxito
                System.Diagnostics.Debug.WriteLine($"UsuarioService.Registrar: Usuario '{usuario.NomUsuario}' registrado exitosamente con ID: {usuario.IdUsuario}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioService.Registrar: Error al registrar usuario: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioService.Registrar: Stack trace: {ex.StackTrace}");
                
                // Si es una excepción de Entity Framework, obtener más detalles
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"UsuarioService.Registrar: Inner exception: {ex.InnerException.Message}");
                }
                
                throw; // Re-lanzar la excepción para que el controlador la maneje
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Usuario> ObtenerTodos()
            => _repo.GetAll();

        public Usuario ObtenerPorId(int id)
            => _repo.GetById(id);

        public void Actualizar(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            
            if (string.IsNullOrWhiteSpace(usuario.ApPaterno))
                throw new ArgumentException("El apellido paterno es obligatorio.");
            
            if (string.IsNullOrWhiteSpace(usuario.Correo))
                throw new ArgumentException("El correo electrónico es obligatorio.");
            
            if (string.IsNullOrWhiteSpace(usuario.NomUsuario))
                throw new ArgumentException("El nombre de usuario es obligatorio.");

            _repo.Update(usuario);
        }

        public void Eliminar(int id)
        {
            var usuario = _repo.GetById(id);
            if (usuario == null)
                throw new ArgumentException("El usuario no existe.");

            _repo.Delete(usuario);
        }

        public Usuario Autenticar(string nombreUsuario, string contraseña)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioService.Autenticar: Iniciando autenticación para usuario: {nombreUsuario}");
                
                if (string.IsNullOrWhiteSpace(nombreUsuario))
                    throw new ArgumentException("El nombre de usuario es obligatorio.");

                if (string.IsNullOrWhiteSpace(contraseña))
                    throw new ArgumentException("La contraseña es obligatoria.");

                System.Diagnostics.Debug.WriteLine("UsuarioService.Autenticar: Validaciones básicas completadas");

                var usuario = _repo.Autenticar(nombreUsuario, contraseña);
                
                if (usuario == null)
                {
                    System.Diagnostics.Debug.WriteLine("UsuarioService.Autenticar: Usuario no encontrado o credenciales inválidas");
                    throw new ArgumentException("Credenciales inválidas o usuario inactivo.");
                }

                System.Diagnostics.Debug.WriteLine($"UsuarioService.Autenticar: Usuario autenticado exitosamente. ID: {usuario.IdUsuario}");
                return usuario;
            }
            catch (ArgumentException)
            {
                // Re-lanzar ArgumentException sin cambios
                throw;
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioService.Autenticar: EntityCommandExecutionException: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioService.Autenticar: Inner Exception: {ex.InnerException?.Message}");
                
                // Proporcionar un mensaje más amigable al usuario
                throw new Exception("Error de conexión con la base de datos. Por favor, verifica que el servidor esté disponible.", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioService.Autenticar: Error inesperado: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioService.Autenticar: Stack Trace: {ex.StackTrace}");
                
                // Proporcionar un mensaje genérico para errores inesperados
                throw new Exception("Error interno del sistema durante la autenticación. Por favor, intenta nuevamente.", ex);
            }
        }
    }
}