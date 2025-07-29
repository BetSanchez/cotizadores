using System.ComponentModel.DataAnnotations;

namespace pr_proyecto.Models
{
    public class PrivilegioRol
    {
        [Key]
        public int IdPrivilegioRol { get; set; }
        public int Privilegio { get; set; }
        public int Rol { get; set; }

        public virtual Privilegio PrivilegioNavigation { get; set; }
        public virtual Rol RolNavigation { get; set; }
    }

}