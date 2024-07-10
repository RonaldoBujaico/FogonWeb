using FogonParillero.Models;

namespace FogonParillero.ViewModel
{
    public class RegistrarProductoViewModel
    {
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public string ImagenUrl { get; set; }
        public int CategoriaProductoId { get; set; }
        public List<CategoriaProducto> Categorias { get; set; }
        public List<Insumo> Insumos { get; set; }
        public List<DetalleInsumo> DetalleInsumos { get; set; }
    }
}
