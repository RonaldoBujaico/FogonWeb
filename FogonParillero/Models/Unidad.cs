using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FogonParillero.Models
{
    [Table("unidad")]
    public class Unidad
    {
        [Key]
        [Column("unidad_id")]
        public int UnidadId { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; }


    }
}
