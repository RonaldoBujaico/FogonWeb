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
        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Required]
        [Column("precio", TypeName = "decimal(10, 2)")]
        public decimal Precio { get; set; }

        [Column("imagen")]
        public string? ImagenUrl { get; set; }

        [Column("categoria_producto_id")]
        public int CategoriaId { get; set; }
        public CategoriaProducto Categoria { get; set; }
    }
}
