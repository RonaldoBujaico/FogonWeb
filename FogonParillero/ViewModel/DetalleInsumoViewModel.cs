using FogonParillero.Models;

namespace FogonParillero.ViewModel
{
    public class DetalleInsumoViewModel
    {
        public IEnumerable<Producto> Productos { get; set; }
        public IEnumerable<Insumo> Insumos { get; set; }
    }
}
