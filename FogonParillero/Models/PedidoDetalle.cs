using System.ComponentModel.DataAnnotations.Schema;

namespace FogonParillero.Models
{
    [Table("PedidoDetalles")]
    public class PedidoDetalle
    {
        [Column("PedidoDetalleId")]
        public int PedidoDetalleId { get; set; }

        [Column("PedidoId")]
        public int PedidoId { get; set; }

        [Column("ProductoId")]
        public int ProductoId { get; set; }

        [Column("Cantidad")]
        public int Cantidad { get; set; }

        [Column("Precio")]
        public decimal Precio { get; set; }

        [ForeignKey("PedidoId")]
        public Pedido Pedido { get; set; }

        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }
    }
}
