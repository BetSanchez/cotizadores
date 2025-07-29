using System.ComponentModel.DataAnnotations;

namespace pr_proyecto.Models
{
    public class PlantillaServicio
    {
        [Key]
        public int IdPlantilla { get; set; }
        public virtual Plantilla Plantilla { get; set; }

        public int IdServicio { get; set; }
        public virtual Servicio Servicio { get; set; }
    }

}