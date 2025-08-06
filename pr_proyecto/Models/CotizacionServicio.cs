using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pr_proyecto.Models
{
    [Table("cotizacion_servicios")]
    public class CotizacionServicio
    {
        [Key]
        [Column("id_cot_servicio")]
        public int IdCotServicio { get; set; }

        [Column("descripcion")]
        [Required]
        public string Descripcion { get; set; }

        [Column("cantidad")]
        [Required]
        public int Cantidad { get; set; }

        [Column("valor_unitario")]
        [Required]
        public double ValorUnitario { get; set; }

        [Column("subtotal")]
        [Required]
        public double Subtotal { get; set; }

        [Column("terminos")]
        public string Terminos { get; set; }

        [Column("incluye")]
        public string Incluye { get; set; }

        [Column("condiciones")]
        public string Condiciones { get; set; }

        [Column("id_cotizacion")]
        [Required]
        public int IdCotizacion { get; set; }

        [ForeignKey(nameof(IdCotizacion))]
        public virtual Cotizacion Cotizacion { get; set; }

        [Column("id_servicio")]
        [Required]
        public int IdServicio { get; set; }

        [ForeignKey(nameof(IdServicio))]
        public virtual Servicio Servicio { get; set; }
    }
}