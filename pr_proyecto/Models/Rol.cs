using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pr_proyecto.Models
{
    public class Rol
    {
        
        [Key]
        [Column("id_rol")] 
        public int IdRol { get; set; }
        [Column("nombre")] 
        public string Nombre { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }

}