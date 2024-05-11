using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FogonParillero.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("nombres")]
        public string Nombres { get; set; }

        [Required]
        [StringLength(50)]
        [Column("apellidos")]
        public string Apellidos { get; set; }

        [Required]
        [StringLength(50)]
        [Column("contraseña")]
        public string Contraseña { get; set; }

        [Required]
        [StringLength(100)]
        [Column("correo")]
        public string Correo { get; set; }

        [Required]
        [StringLength(20)]
        [Column("dni")]
        public string Dni { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        [Column("fecha_nacimiento")]
        public DateTime FechaNacimiento { get; set; }
    }
}
