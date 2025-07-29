using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pr_proyecto.Models
{
    public class Cotizacion
    {
        [Key]
        public int IdCotizacion { get; set; }
        public string Folio { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int IdUsuarioVendedor { get; set; }
        public string Nota { get; set; }
        public double Subtotal { get; set; }
        public double Iva { get; set; }
        public double Total { get; set; }
        public char Estatus { get; set; }
        public int Version { get; set; }
        public int UltimaVersion { get; set; }
        public DateTime FechaGuardado { get; set; }
        public string Comentario { get; set; }

        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        public int IdSucursal { get; set; }
        public virtual Sucursal Sucursal { get; set; }

        public virtual ICollection<CotizacionServicio> Servicios { get; set; }
    }

}