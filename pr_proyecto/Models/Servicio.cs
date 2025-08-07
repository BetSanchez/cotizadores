using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pr_proyecto.Models
{
    [Table("servicios")]
    public class Servicio
    {
        [Key]
        [Column("id_servicio")]
        public int IdServicio { get; set; }
        
        [Column("nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }
        
        [Column("descripcion")]
        [MaxLength(500)]
        public string Descripcion { get; set; }
        
        [Column("terminos")]
        [MaxLength(1000)]
        public string Terminos { get; set; }
        
        [Column("incluye")]
        [MaxLength(1000)]
        public string Incluye { get; set; }
        
        [Column("condiciones")]
        [MaxLength(1000)]
        public string Condiciones { get; set; }

        public virtual ICollection<CotizacionServicio> CotizacionServicios { get; set; } = new List<CotizacionServicio>();
        public virtual ICollection<PlantillaServicio> PlantillaServicios { get; set; } = new List<PlantillaServicio>();
    }
}