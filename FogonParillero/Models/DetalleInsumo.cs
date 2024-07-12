using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FogonParillero.Models
{
    [Table("detalle_insumo")]
    public class DetalleInsumo
    {
        [Key]
        [Column("detalle_insumo_id")]
        public int DetalleInsumoId { get; set; }

        [Required]
        [Column("producto_id")]
        public int ProductoId { get; set; }

        [Required]
        [Column("insumo_id")]
        public int InsumoId { get; set; }
        public Insumo Insumo { get; set; }

        [Required]
        [Column("cantidad", TypeName = "decimal(15, 2)")]
        public decimal Cantidad { get; set; }
    }
}
