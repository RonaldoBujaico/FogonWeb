using FogonParillero.Models;

namespace FogonParillero.Interfaces
{
    public interface ICategoriaInsumoInterface
    {
        Task<IEnumerable<CategoriaInsumo>> ObtenerTodosAsync();
        Task<CategoriaInsumo> ObtenerPorId(int id);
        Task<CategoriaInsumo> Registrar(string nombre);
        Task<CategoriaInsumo> Actualizar(CategoriaInsumo categoriaInsumo);
        Task<CategoriaInsumo> Eliminar(CategoriaInsumo categoriaInsumo);
    }
}
