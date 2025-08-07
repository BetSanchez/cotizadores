using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pr_proyecto.Models
{
    [Table("plantillas")]
    public class Plantilla
    {
        [Key]
        [Column("id_plantilla")]
        public int IdPlantilla { get; set; }

        [Column("nombre")]
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Column("fecha_creacion")]
        [Required]
        public DateTime FechaCreacion { get; set; }

        [Column("id_usuario")]
        [Required]
        public int IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<PlantillaServicio> PlantillaServicios { get; set; } = new List<PlantillaServicio>();
    }
}