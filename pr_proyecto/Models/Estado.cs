using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pr_proyecto.Models
{
    public class Estado
    {
        
        [Key]
        public int IdEstado { get; set; }
        public string Nombre { get; set; }
    
        public int IdPais { get; set; }
        public virtual Pais Pais { get; set; }

        public virtual ICollection<Municipio> Municipios { get; set; }
    }
}