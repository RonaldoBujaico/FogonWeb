using FogonParillero.Models;

namespace FogonParillero.Interfaces
{
    public interface IDetalleInsumoInterface
    {
        Task<bool> AddDetalleInsumo(DetalleInsumo detalleInsumo);
    }
}
