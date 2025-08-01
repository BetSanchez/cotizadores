using System;
using System.Web;
namespace pr_proyecto.utils
{



    public static class AuthHelper
    {
        /// <summary>
        /// Verifica si el usuario está autenticado
        /// </summary>
        /// <returns>True si está autenticado, false en caso contrario</returns>
        public static bool IsAuthenticated()
        {
            return HttpContext.Current.Session["UsuarioId"] != null;
        }

        /// <summary>
        /// Obtiene el ID del usuario autenticado
        /// </summary>
        /// <returns>ID del usuario o null si no está autenticado</returns>
        public static int? GetCurrentUserId()
        {
            var userId = HttpContext.Current.Session["UsuarioId"];
            return userId != null ? (int)userId : (int?)null;
        }

        /// <summary>
        /// Obtiene el nombre de usuario autenticado
        /// </summary>
        /// <returns>Nombre de usuario o null si no está autenticado</returns>
        public static string GetCurrentUsername()
        {
            return HttpContext.Current.Session["NombreUsuario"]?.ToString();
        }

        /// <summary>
        /// Obtiene el nombre completo del usuario autenticado
        /// </summary>
        /// <returns>Nombre completo o null si no está autenticado</returns>
        public static string GetCurrentUserFullName()
        {
            return HttpContext.Current.Session["NombreCompleto"]?.ToString();
        }

        /// <summary>
        /// Obtiene el rol del usuario autenticado
        /// </summary>
        /// <returns>Rol del usuario o null si no está autenticado</returns>
        public static string GetCurrentUserRole()
        {
            return HttpContext.Current.Session["Rol"]?.ToString();
        }

        /// <summary>
        /// Obtiene el ID del rol del usuario autenticado
        /// </summary>
        /// <returns>ID del rol o null si no está autenticado</returns>
        public static int? GetCurrentUserRoleId()
        {
            var roleId = HttpContext.Current.Session["RolId"];
            return roleId != null ? (int)roleId : (int?)null;
        }

        /// <summary>
        /// Verifica si el usuario tiene un rol específico
        /// </summary>
        /// <param name="roleName">Nombre del rol a verificar</param>
        /// <returns>True si tiene el rol, false en caso contrario</returns>
        public static bool HasRole(string roleName)
        {
            var currentRole = GetCurrentUserRole();
            return !string.IsNullOrEmpty(currentRole) && 
                   currentRole.Equals(roleName, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Verifica si el usuario tiene uno de los roles especificados
        /// </summary>
        /// <param name="roleNames">Nombres de los roles a verificar</param>
        /// <returns>True si tiene al menos uno de los roles, false en caso contrario</returns>
        public static bool HasAnyRole(params string[] roleNames)
        {
            var currentRole = GetCurrentUserRole();
            if (string.IsNullOrEmpty(currentRole))
                return false;

            foreach (var roleName in roleNames)
            {
                if (currentRole.Equals(roleName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Redirige al login si el usuario no está autenticado
        /// </summary>
        public static void RequireAuthentication()
        {
            if (!IsAuthenticated())
            {
                HttpContext.Current.Response.Redirect("~/Views/Login.aspx");
            }
        }

        /// <summary>
        /// Redirige al login si el usuario no tiene el rol especificado
        /// </summary>
        /// <param name="roleName">Nombre del rol requerido</param>
        public static void RequireRole(string roleName)
        {
            RequireAuthentication();
            if (!HasRole(roleName))
            {
                HttpContext.Current.Response.Redirect("~/Views/Login.aspx");
            }
        }

        /// <summary>
        /// Redirige al login si el usuario no tiene al menos uno de los roles especificados
        /// </summary>
        /// <param name="roleNames">Nombres de los roles requeridos</param>
        public static void RequireAnyRole(params string[] roleNames)
        {
            RequireAuthentication();
            if (!HasAnyRole(roleNames))
            {
                HttpContext.Current.Response.Redirect("~/Views/Login.aspx");
            }
        }
    }
} 
