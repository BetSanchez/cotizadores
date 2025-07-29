using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pr_proyecto.Models
{
    public class Pais
    {
        [Key]
        public int IdPais { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Estado> Estados { get; set; }
    }

}