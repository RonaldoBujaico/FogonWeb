using FogonParillero.Models;

namespace FogonParillero.Interfaces
{
    public interface IInsumoInterface
    {
        Task<IEnumerable<Insumo>> ObtenerTodosAsync();
    }
}
