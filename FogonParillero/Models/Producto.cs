using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FogonParillero.Models
{
    [Table("producto")]
    public class Producto
    {
        [Key]
        [Column("producto_id")]
        public int ProductoId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [StringLength(255)]
        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Required]
        [Column("precio", TypeName = "decimal(10, 2)")]
        public decimal Precio { get; set; }

        [StringLength(255)]
        [Column("imagen_url")]
        public string ImagenUrl { get; set; }

        [Required]
        [Column("categoria_id")]
        public int CategoriaId { get; set; }
        public CategoriaProducto Categoria { get; set; }
    }
}
