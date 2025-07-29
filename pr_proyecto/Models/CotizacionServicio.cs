using System.ComponentModel.DataAnnotations;

namespace pr_proyecto.Models
{
    public class CotizacionServicio
    {
        [Key]
        public int IdCotServicio { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public double ValorUnitario { get; set; }
        public double Subtotal { get; set; }
        public string Terminos { get; set; }
        public string Incluye { get; set; }
        public string Condiciones { get; set; }

        public int IdCotizacion { get; set; }
        public virtual Cotizacion Cotizacion { get; set; }

        public int IdServicio { get; set; }
        public virtual Servicio Servicio { get; set; }
    }

}