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
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Column("ap_paterno")]
        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        [MaxLength(100)]
        public string ApPaterno { get; set; }

        [Column("ap_materno")]
        [MaxLength(100)]
        public string ApMaterno { get; set; }

        [Column("correo")]
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        [MaxLength(100)]
        public string Correo { get; set; }

        [Column("telefono")]
        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [MaxLength(15)]
        public string Telefono { get; set; }

        [Column("tel_empresa")]
        [MaxLength(15)]
        public string TelEmpresa { get; set; }

        [Column("carrera")]
        [Required(ErrorMessage = "La carrera es obligatoria")]
        [MaxLength(100)]
        public string Carrera { get; set; }

        [Column("direccion")]
        [Required(ErrorMessage = "La dirección es obligatoria")]
        [MaxLength(200)]
        public string Direccion { get; set; }

        [Column("fecha_activacion")]
        [Required(ErrorMessage = "La fecha de activación es obligatoria")]
        public DateTime FechaActivacion { get; set; }

        [Column("fecha_ingreso")]
        public DateTime? FechaIngreso { get; set; }

        [Column("fecha_termino")]
        public DateTime? FechaTermino { get; set; }

        [Column("nom_usuario")]
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [MaxLength(50)]
        public string NomUsuario { get; set; }

        [Column("contrasea")]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MaxLength(100)]
        public string Contrasena { get; set; }


        [Column("estatus")]
        [Required(ErrorMessage = "El estatus es obligatorio")]
        public bool Estatus { get; set; }

        [Column("id_rol")]
        [Required(ErrorMessage = "El rol es obligatorio")]
        public int IdRol { get; set; }

        [ForeignKey(nameof(IdRol))]
        public virtual Rol Rol { get; set; }

        public virtual ICollection<Cotizacion> Cotizaciones { get; set; } = new List<Cotizacion>();
        public virtual ICollection<Plantilla> Plantillas { get; set; } = new List<Plantilla>();
    }
}