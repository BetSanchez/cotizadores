using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Services
{
    public interface IUsuarioService
    {
        void Registrar(Usuario usuario);
        IEnumerable<Usuario> ObtenerTodos();
        Usuario Autenticar(string nombreUsuario, string contraseña);
    }
}