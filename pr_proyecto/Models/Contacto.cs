using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pr_proyecto.Models
{
    [Table("contactos")] 
    public class Contacto
    {
        [Key]
        [Column("id_contacto")]
        public int IdContacto { get; set; }

        [Required]
        [Column("nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [Column("ap_paterno")]
        [MaxLength(100)]
        public string ApPaterno { get; set; }

        [Column("ap_materno")]
        [MaxLength(100)]
        public string ApMaterno { get; set; }

        [Required]
        [Column("correo")]
        [MaxLength(100)]
        public string Correo { get; set; }

        [Required]
        [Column("telefono")]
        [MaxLength(15)]
        public string Telefono { get; set; }

        [Required]
        [Column("puesto")]
        [MaxLength(100)]
        public string Puesto { get; set; }

        [Required]
        [Column("id_sucursal")]
        public int IdSucursal { get; set; }

        // Relación con Sucursal
        [ForeignKey("IdSucursal")]
        public virtual Sucursal Sucursal { get; set; }
    }
}