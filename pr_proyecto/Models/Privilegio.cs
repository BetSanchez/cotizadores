using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pr_proyecto.Models
{
    public class Privilegio
    {
        
        [Key]
        public int IdPrivilegio { get; set; }
        public string Nombre { get; set; }
        public int Valor { get; set; }

        public virtual ICollection<PrivilegioRol> PrivilegiosRoles { get; set; }
    }
}