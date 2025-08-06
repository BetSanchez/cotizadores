using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pr_proyecto.Models
{
    [Table("cotizaciones")]
    public class Cotizacion
    {
        [Key]
        [Column("id_cotizacion")]
        public int IdCotizacion { get; set; }

        [Column("folio")]
        [Required]
        [MaxLength(50)]
        public string Folio { get; set; }

        [Column("fecha_emision")]
        [Required]
        public DateTime FechaEmision { get; set; }

        [Column("fecha_vencimiento")]
        [Required]
        public DateTime FechaVencimiento { get; set; }

        [Column("id_usuario_vendedor")]
        [Required]
        public int IdUsuarioVendedor { get; set; }

        [ForeignKey(nameof(IdUsuarioVendedor))]
        public virtual Usuario UsuarioVendedor { get; set; }

        [Column("nota")]
        public string Nota { get; set; }

        [Column("subtotal")]
        [Required]
        public double Subtotal { get; set; }

        [Column("iva")]
        [Required]
        public double Iva { get; set; }

        [Column("total")]
        [Required]
        public double Total { get; set; }

        [Column("estatus")]
        [Required]
        [MaxLength(1)]
        public string Estatus { get; set; }

        [Column("version")]
        [Required]
        public int Version { get; set; }

        [Column("ultima_version")]
        [Required]
        public int UltimaVersion { get; set; }

        [Column("fecha_guardado")]
        [Required]
        public DateTime FechaGuardado { get; set; }

        [Column("comentario")]
        public string Comentario { get; set; }

        [Column("id_usuario")]
        [Required]
        public int IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        public virtual Usuario Usuario { get; set; }

        [Column("id_sucursal")]
        [Required]
        public int IdSucursal { get; set; }

        [ForeignKey(nameof(IdSucursal))]
        public virtual Sucursal Sucursal { get; set; }

        public virtual ICollection<CotizacionServicio> Servicios { get; set; } = new List<CotizacionServicio>();
    }
}