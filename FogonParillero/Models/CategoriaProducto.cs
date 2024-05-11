using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FogonParillero.Models
{

    [Table("categoria_producto")]
    public class CategoriaProducto
    {
        [Key]
        [Column("categoria_id")]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [StringLength(255)]
        [Column("imagen_url")]
        public string ImagenUrl { get; set; }
    }
}
