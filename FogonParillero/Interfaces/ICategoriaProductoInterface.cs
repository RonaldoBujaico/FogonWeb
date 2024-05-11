using FogonParillero.Models;

namespace FogonParillero.Interfaces
{
    public interface ICategoriaProductoInterface
    {
        Task<IEnumerable<CategoriaProducto>> ObtenerTodosAsync();
        Task<CategoriaProducto> ObtenerPorId(int id);
        Task<CategoriaProducto> Registrar(string nombre, string imagen);
        Task<CategoriaProducto> Actualizar(CategoriaProducto categoriaProducto);
        Task<CategoriaProducto> Eliminar(CategoriaProducto categoriaProducto);
    }
}
