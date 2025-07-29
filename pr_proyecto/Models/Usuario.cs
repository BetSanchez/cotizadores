using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pr_proyecto.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("ap_paterno")]
        public string ApPaterno { get; set; }

        [Column("ap_materno")]
        public string ApMaterno { get; set; }

        [Column("correo")]
        public string Correo { get; set; }

        [Column("telefono")]
        public string Telefono { get; set; }

        [Column("tel_empresa")]
        public string TelEmpresa { get; set; }

        [Column("carrera")]
        public string Carrera { get; set; }

        [Column("direccion")]
        public string Direccion { get; set; }

        [Column("fecha_activacion")]
        public DateTime FechaActivacion { get; set; }

        [Column("fecha_ingreso")]
        public DateTime? FechaIngreso { get; set; }

        [Column("fecha_termino")]
        public DateTime? FechaTermino { get; set; }

        [Column("nom_usuario")]
        public string NomUsuario { get; set; }

        [Column("contraseña")]
        public string Contraseña { get; set; }

        [Column("estatus")]
        public bool Estatus { get; set; }

        [Column("id_rol")]
        public int IdRol { get; set; }

        [ForeignKey(nameof(IdRol))]
        public virtual Rol Rol { get; set; }

        public virtual ICollection<Cotizacion> Cotizaciones { get; set; }
        public virtual ICollection<Plantilla> Plantillas { get; set; }
    }
}