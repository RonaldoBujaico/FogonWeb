using FogonParillero.Models;

namespace FogonParillero.Interfaces
{
    public interface IUnidadInterface
    {

        Task<IEnumerable<Unidad>> ObtenerTodosAsync();
    }
}
