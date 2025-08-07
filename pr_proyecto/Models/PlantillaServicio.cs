using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pr_proyecto.Models
{
    [Table("plantilla_servicios")]
    public class PlantillaServicio
    {
        [Key]
        [Column("id_plantilla")]
        public int IdPlantilla { get; set; }

        [ForeignKey(nameof(IdPlantilla))]
        public virtual Plantilla Plantilla { get; set; }

        [Key]
        [Column("id_servicio")]
        public int IdServicio { get; set; }

        [ForeignKey(nameof(IdServicio))]
        public virtual Servicio Servicio { get; set; }
    }
}