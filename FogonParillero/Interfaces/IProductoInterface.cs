using FogonParillero.Models;

namespace FogonParillero.Interfaces
{
    public interface IProductoInterface
    {
        Task<IEnumerable<Producto>> ObtenerTodosAsync();
        Task<Producto> ObtenerPorId(int id);
        Task<Producto> Registrar(Producto producto);
        Task<Producto> Actualizar(Producto producto);
        Task<Producto> Eliminar(Producto producto);
    }
}
