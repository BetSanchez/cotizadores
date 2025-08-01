using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pr_proyecto.Models
{
    [Table("sucursales")] 
    public class Sucursal
    {
        [Key]
        [Column("id_sucursal")]
        public int IdSucursal { get; set; }

        [Required]
        [Column("nom_comercial")]
        [MaxLength(100)]
        public string NomComercial { get; set; }

        [Column("razon_social")]
        [MaxLength(150)]
        public string RazonSocial { get; set; }

        [Column("giro")]
        [MaxLength(100)]
        public string Giro { get; set; }

        [Required]
        [Column("telefono")]
        [MaxLength(15)]
        public string Telefono { get; set; }

        [Required]
        [Column("correo")]
        [MaxLength(100)]
        public string Correo { get; set; }

        [Required]
        [Column("direccion")]
        [MaxLength(200)]
        public string Direccion { get; set; }

        [Column("metros")]
        public double? Metros { get; set; }

        [Required]
        [Column("id_empresa")]
        public int IdEmpresa { get; set; }

        [ForeignKey("IdEmpresa")]
        public virtual Empresa Empresa { get; set; }

        [Required]
        [Column("id_colonia")]
        public int IdColonia { get; set; }

        [ForeignKey("IdColonia")]
        public virtual Colonia Colonia { get; set; }

        public virtual ICollection<Contacto> Contactos { get; set; } = new List<Contacto>();
        public virtual ICollection<Cotizacion> Cotizaciones { get; set; } = new List<Cotizacion>();
    }
}