using FogonParillero.Models;

namespace FogonParillero.ViewModel
{
    public class InsumoViewModel
    {

        public IEnumerable<Insumo> Insumos { get; set; }
        public IEnumerable<CategoriaInsumo> Categorias { get; set; }
        public Insumo Insumo { get; set; }
        public IEnumerable<Unidad> Unidades { get; set; }

    }
}
