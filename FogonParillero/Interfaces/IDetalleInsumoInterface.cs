using FogonParillero.Models;

namespace FogonParillero.Interfaces
{
    public interface IDetalleInsumoInterface
    {
        Task<bool> AddDetalleInsumo(DetalleInsumo detalleInsumo);
        Task<IEnumerable<DetalleInsumo>> ObtenerDetallesInsumo();
        Task<IEnumerable<DetalleInsumo>> ObtenerDetalleInsumo(int productoId);
    }
}
