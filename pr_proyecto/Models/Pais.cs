using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pr_proyecto.Models
{
    [Table("paises")]
    public class Pais
    {
        [Key]
        [Column("id_pais")]
        public int IdPais { get; set; }
        
        [Column("nombre")]
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        public virtual ICollection<Estado> Estados { get; set; }
    }
}