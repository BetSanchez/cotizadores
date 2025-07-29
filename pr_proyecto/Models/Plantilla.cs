using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pr_proyecto.Models
{
    public class Plantilla
    {
        
        [Key]
        public int IdPlantilla { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }

        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<PlantillaServicio> PlantillaServicios { get; set; }
    }

}