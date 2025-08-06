using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pr_proyecto.Models
{
    [Table("roles")]
    public class Rol
    {
        [Key]
        [Column("id_rol")] 
        public int IdRol { get; set; }
        
        [Column("nombre")]
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}