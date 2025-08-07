using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Services
{
    public interface IUsuarioService
    {
        void Registrar(Usuario usuario);
        IEnumerable<Usuario> ObtenerTodos();
        Usuario ObtenerPorId(int id);
        void Actualizar(Usuario usuario);
        void Eliminar(int id);
        Usuario Autenticar(string nombreUsuario, string contraseña);
    }
}