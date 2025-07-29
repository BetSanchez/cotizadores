using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pr_proyecto.Models
{
    public class Servicio
    {
        
        [Key]
        public int IdServicio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Terminos { get; set; }
        public string Incluye { get; set; }
        public string Condiciones { get; set; }

        public virtual ICollection<CotizacionServicio> CotizacionServicios { get; set; }
        public virtual ICollection<PlantillaServicio> PlantillaServicios { get; set; }
    }

}