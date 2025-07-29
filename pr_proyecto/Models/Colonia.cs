using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pr_proyecto.Models
{
    public class Colonia
    {
        [Key]
        public int IdColonia { get; set; }
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public string Asentamiento { get; set; }
        public string CodigoPostal { get; set; }

        public int IdMunicipio { get; set; }
        public virtual Municipio Municipio { get; set; }

        public virtual ICollection<Sucursal> Sucursales { get; set; }
    }

}