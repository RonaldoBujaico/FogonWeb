using FogonParillero.Models;

namespace FogonParillero.ViewModel
{
    public class ProductoViewModel
    {
        public IEnumerable<Producto> Productos { get; set; }
        public IEnumerable<Insumo> Insumos { get; set; }
        public IEnumerable<CategoriaProducto> Categorias { get; set; }
        public Producto Producto { get; set; }
        public List<Mesa> Mesa { get; set; }
        public List<Pedido> Pedidos { get; set; }
        public IEnumerable<DetalleInsumo> DetalleInsumos { get; set; }

    }
    public class GuardarPedidoViewModel
    {
        public string MesaId { get; set; }
        public DateTime fechapedido { get; set; }  // Nuevo campo para la hora del pedido

        public List<ProductoPedidoViewModel> Productos { get; set; }
    }

    public class ProductoPedidoViewModel
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
