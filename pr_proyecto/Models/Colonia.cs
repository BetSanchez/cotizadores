using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pr_proyecto.Models
{
    [Table("colonias")]
    public class Colonia
    {
        [Key]
        [Column("id_colonia")]
        public int IdColonia { get; set; }
        
        [Column("nombre")]
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        
        [Column("ciudad")]
        [Required]
        [MaxLength(100)]
        public string Ciudad { get; set; }
        
        [Column("asentamiento")]
        [MaxLength(100)]
        public string Asentamiento { get; set; }
        
        [Column("codigo_postal")]
        [MaxLength(10)]
        public string CodigoPostal { get; set; }

        [Column("id_municipio")]
        [Required]
        public int IdMunicipio { get; set; }
        
        [ForeignKey("IdMunicipio")]
        public virtual Municipio Municipio { get; set; }

        public virtual ICollection<Sucursal> Sucursales { get; set; } = new List<Sucursal>();
    }
}