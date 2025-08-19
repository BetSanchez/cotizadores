using System;
using System.Collections.Generic;
using System.Linq;
using pr_proyecto.Data;
using pr_proyecto.Models;
using pr_proyecto.Repository;
using System.Data.Entity;

namespace pr_proyecto.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public Usuario GetById(int id)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetById: Obteniendo usuario con ID: {id}");
                
                // Verificar que el contexto esté disponible
                if (_context == null)
                {
                    System.Diagnostics.Debug.WriteLine("UsuarioRepository.GetById: ERROR - Contexto es null");
                    throw new InvalidOperationException("El contexto de base de datos no está disponible");
                }

                // Verificar que la conexión esté disponible
                if (_context.Database.Connection.State != System.Data.ConnectionState.Open)
                {
                    System.Diagnostics.Debug.WriteLine("UsuarioRepository.GetById: Abriendo conexión a la base de datos");
                    _context.Database.Connection.Open();
                }

                var usuario = _context.Usuarios.Find(id);
                if (usuario != null)
                {
                    System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetById: Usuario encontrado: {usuario.NomUsuario}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetById: No se encontró usuario con ID: {id}");
                }
                return usuario;
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetById: EntityCommandExecutionException: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetById: Inner Exception: {ex.InnerException?.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetById: Stack Trace: {ex.StackTrace}");
                
                // Intentar obtener más detalles del error
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetById: Inner Exception Stack Trace: {ex.InnerException.StackTrace}");
                }
                
                throw new Exception($"Error al ejecutar la consulta de usuario por ID: {ex.Message}", ex);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetById: SqlException: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetById: Error Number: {ex.Number}");
                throw new Exception($"Error de base de datos: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetById: Error general: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetById: Stack Trace: {ex.StackTrace}");
                throw new Exception($"Error inesperado al obtener usuario por ID: {ex.Message}", ex);
            }
        }

        public IEnumerable<Usuario> GetAll()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("UsuarioRepository.GetAll: Obteniendo todos los usuarios");
                
                // Verificar que el contexto esté disponible
                if (_context == null)
                {
                    System.Diagnostics.Debug.WriteLine("UsuarioRepository.GetAll: ERROR - Contexto es null");
                    throw new InvalidOperationException("El contexto de base de datos no está disponible");
                }

                // Verificar que la conexión esté disponible
                if (_context.Database.Connection.State != System.Data.ConnectionState.Open)
                {
                    System.Diagnostics.Debug.WriteLine("UsuarioRepository.GetAll: Abriendo conexión a la base de datos");
                    _context.Database.Connection.Open();
                }

                // Intentar una consulta más específica para debugging
                System.Diagnostics.Debug.WriteLine("UsuarioRepository.GetAll: Intentando consulta con Include");
                var usuarios = _context.Usuarios
                    .Include(u => u.Rol)
                    .ToList();
                
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetAll: Se obtuvieron {usuarios.Count} usuarios");
                return usuarios;
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetAll: EntityCommandExecutionException: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetAll: Inner Exception: {ex.InnerException?.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetAll: Stack Trace: {ex.StackTrace}");
                
                // Intentar obtener más detalles del error
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetAll: Inner Exception Stack Trace: {ex.InnerException.StackTrace}");
                }
                
                throw new Exception($"Error al ejecutar la consulta de usuarios: {ex.Message}", ex);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetAll: SqlException: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetAll: Error Number: {ex.Number}");
                throw new Exception($"Error de base de datos: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetAll: Error general: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.GetAll: Stack Trace: {ex.StackTrace}");
                throw new Exception($"Error inesperado al obtener usuarios: {ex.Message}", ex);
            }
        }

        public void Add(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Extrae el mensaje más profundo de la InnerException
                var deepest = ex.InnerException?.InnerException?.Message
                              ?? ex.InnerException?.Message
                              ?? ex.Message;
                // Relanza una excepción con ese mensaje para que la veas en la UI o en el Debug
                throw new Exception($"Error al guardar Usuario: {deepest}", ex);
            }
        }


        public void Delete(Usuario usuario)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Delete: Eliminando usuario '{usuario.NomUsuario}' (ID: {usuario.IdUsuario})");
                
                // Verificar que el contexto esté disponible
                if (_context == null)
                {
                    System.Diagnostics.Debug.WriteLine("UsuarioRepository.Delete: ERROR - Contexto es null");
                    throw new InvalidOperationException("El contexto de base de datos no está disponible");
                }

                // Verificar que la conexión esté disponible
                if (_context.Database.Connection.State != System.Data.ConnectionState.Open)
                {
                    System.Diagnostics.Debug.WriteLine("UsuarioRepository.Delete: Abriendo conexión a la base de datos");
                    _context.Database.Connection.Open();
                }

                _context.Usuarios.Remove(usuario);
                var result = _context.SaveChanges();
                
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Delete: SaveChanges completado. Filas afectadas: {result}");
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Delete: EntityCommandExecutionException: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Delete: Inner Exception: {ex.InnerException?.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Delete: Stack Trace: {ex.StackTrace}");
                
                // Intentar obtener más detalles del error
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Delete: Inner Exception Stack Trace: {ex.InnerException.StackTrace}");
                }
                
                throw new Exception($"Error al ejecutar el comando de eliminación: {ex.Message}", ex);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Delete: SqlException: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Delete: Error Number: {ex.Number}");
                throw new Exception($"Error de base de datos: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Delete: Error general: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Delete: Stack Trace: {ex.StackTrace}");
                throw new Exception($"Error inesperado durante la eliminación: {ex.Message}", ex);
            }
        }
    

        public void Update(Usuario usuario)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Update: Actualizando usuario '{usuario.NomUsuario}' (ID: {usuario.IdUsuario})");
                
                // Verificar que el contexto esté disponible
                if (_context == null)
                {
                    System.Diagnostics.Debug.WriteLine("UsuarioRepository.Update: ERROR - Contexto es null");
                    throw new InvalidOperationException("El contexto de base de datos no está disponible");
                }

                // Verificar que la conexión esté disponible
                if (_context.Database.Connection.State != System.Data.ConnectionState.Open)
                {
                    System.Diagnostics.Debug.WriteLine("UsuarioRepository.Update: Abriendo conexión a la base de datos");
                    _context.Database.Connection.Open();
                }

                _context.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                var result = _context.SaveChanges();
                
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Update: SaveChanges completado. Filas afectadas: {result}");
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Update: EntityCommandExecutionException: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Update: Inner Exception: {ex.InnerException?.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Update: Stack Trace: {ex.StackTrace}");
                
                // Intentar obtener más detalles del error
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Update: Inner Exception Stack Trace: {ex.InnerException.StackTrace}");
                }
                
                throw new Exception($"Error al ejecutar el comando de actualización: {ex.Message}", ex);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Update: SqlException: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Update: Error Number: {ex.Number}");
                throw new Exception($"Error de base de datos: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Update: Error general: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Update: Stack Trace: {ex.StackTrace}");
                throw new Exception($"Error inesperado durante la actualización: {ex.Message}", ex);
            }
        }

        public Usuario Autenticar(string nombreUsuario, string contraseña)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Autenticar: Iniciando autenticación para usuario: {nombreUsuario}");
                
                // Encriptar la contraseña de la misma manera que en el registro
                string contraseñaEncriptada = System.Convert.ToBase64String(
                    System.Text.Encoding.UTF8.GetBytes(contraseña));

                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Autenticar: Contraseña encriptada generada");

                // Verificar que el contexto esté disponible
                if (_context == null)
                {
                    System.Diagnostics.Debug.WriteLine("UsuarioRepository.Autenticar: ERROR - Contexto es null");
                    throw new InvalidOperationException("El contexto de base de datos no está disponible");
                }

                // Verificar que la conexión esté disponible
                if (_context.Database.Connection.State != System.Data.ConnectionState.Open)
                {
                    System.Diagnostics.Debug.WriteLine("UsuarioRepository.Autenticar: Abriendo conexión a la base de datos");
                    _context.Database.Connection.Open();
                }

                System.Diagnostics.Debug.WriteLine("UsuarioRepository.Autenticar: Ejecutando consulta de autenticación");

                var usuario = _context.Usuarios
                    .Include("Rol")
                    .FirstOrDefault(u => u.NomUsuario == nombreUsuario &&
                                         u.Contrasena == contraseñaEncriptada &&
                                         u.Estatus == true);

                if (usuario != null)
                {
                    System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Autenticar: Usuario autenticado exitosamente. ID: {usuario.IdUsuario}, Rol: {usuario.Rol?.Nombre}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("UsuarioRepository.Autenticar: Usuario no encontrado o credenciales inválidas");
                }

                return usuario;
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Autenticar: EntityCommandExecutionException: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Autenticar: Inner Exception: {ex.InnerException?.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Autenticar: Stack Trace: {ex.StackTrace}");
                
                // Intentar obtener más detalles del error
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Autenticar: Inner Exception Stack Trace: {ex.InnerException.StackTrace}");
                }
                
                throw new Exception($"Error al ejecutar la consulta de autenticación: {ex.Message}", ex);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Autenticar: SqlException: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Autenticar: Error Number: {ex.Number}");
                throw new Exception($"Error de base de datos: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Autenticar: Error general: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UsuarioRepository.Autenticar: Stack Trace: {ex.StackTrace}");
                throw new Exception($"Error inesperado durante la autenticación: {ex.Message}", ex);
            }
        }
    }
}