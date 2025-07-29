using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pr_proyecto.Models
{
    public class Municipio
    {
        
        [Key]
        public int IdMunicipio { get; set; }
        public string Nombre { get; set; }

        public int IdEstado { get; set; }
        public virtual Estado Estado { get; set; }

        public virtual ICollection<Colonia> Colonias { get; set; }
    }
}