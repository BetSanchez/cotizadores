using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pr_proyecto.Models
{
    [Table("municipios")]
    public class Municipio
    {
        [Key]
        [Column("id_municipio")]
        public int IdMunicipio { get; set; }
        
        [Column("nombre")]
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Column("id_estado")]
        [Required]
        public int IdEstado { get; set; }
        
        [ForeignKey("IdEstado")]
        public virtual Estado Estado { get; set; }

        public virtual ICollection<Colonia> Colonias { get; set; }
    }
}