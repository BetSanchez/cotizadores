using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pr_proyecto.Models
{
    [Table("estados")]
    public class Estado
    {
        [Key]
        [Column("id_estado")]
        public int IdEstado { get; set; }
        
        [Column("nombre")]
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
    
        [Column("id_pais")]
        [Required]
        public int IdPais { get; set; }
        
        [ForeignKey("IdPais")]
        public virtual Pais Pais { get; set; }

        public virtual ICollection<Municipio> Municipios { get; set; }
    }
}