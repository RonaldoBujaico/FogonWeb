using FogonParillero.Models;

namespace FogonParillero.Interfaces
{
    public interface IInsumoInterface
    {
        Task<IEnumerable<Insumo>> ObtenerTodosAsync();
        Task<Insumo> ObtenerPorId(int id);
        Task<Insumo> Registrar(string nombre, string descrip, int cant, string img, int idcatinsu, int unidad);
        Task<Insumo> Actualizar(Insumo Insumo);
        Task<Insumo> Eliminar(Insumo Insumo);


    }
}
