using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FogonParillero.Models
{
    [Table("Mesa")]
    public class Mesa
    {
        [Key]
        [Column("mesa_id")]
        [MaxLength(20)]
        public string MesaId { get; set; }

        [Required]
        [Column("numero_mesa")]
        public int NumeroMesa { get; set; }

        [Required]
        [Column("estado")]
        [MaxLength(50)]
        public string Estado { get; set; }

        [Required]
        [Column("usu_creador")]
        [MaxLength(100)]
        public string UsuCreador { get; set; }

        [Required]
        [Column("fech_creador")]
        public DateTime FechCreador { get; set; }

        [Column("usu_modifica")]
        [MaxLength(100)]
        public string UsuModifica { get; set; }

        [Column("fech_modifica")]
        public DateTime? FechModifica { get; set; }
    }
}
