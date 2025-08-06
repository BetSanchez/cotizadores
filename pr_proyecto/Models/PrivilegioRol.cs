using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pr_proyecto.Models
{
    [Table("privilegio_rol")]
    public class PrivilegioRol
    {
        [Key]
        [Column("id_privilegio_rol")]
        public int IdPrivilegioRol { get; set; }

        [Column("privilegio")]
        [Required]
        public int Privilegio { get; set; }

        [ForeignKey(nameof(Privilegio))]
        public virtual Privilegio PrivilegioNavigation { get; set; }

        [Column("rol")]
        [Required]
        public int Rol { get; set; }

        [ForeignKey(nameof(Rol))]
        public virtual Rol RolNavigation { get; set; }
    }
}