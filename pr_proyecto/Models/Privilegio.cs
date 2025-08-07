using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pr_proyecto.Models
{
    [Table("privilegios")]
    public class Privilegio
    {
        [Key]
        [Column("id_privilegio")]
        public int IdPrivilegio { get; set; }

        [Column("nombre")]
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Column("privilegio")]
        [Required]
        public int Valor { get; set; }

        public virtual ICollection<PrivilegioRol> PrivilegiosRoles { get; set; } = new List<PrivilegioRol>();
    }
}