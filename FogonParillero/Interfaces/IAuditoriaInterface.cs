using FogonParillero.Models;

namespace FogonParillero.Interfaces
{
    public interface IAuditoriaInterface
    {
        Task<IEnumerable<Auditoria>> ObtenerTodosAsync();
        Task<Auditoria> ObtenerPorId(int id);
        Task<Auditoria> Registrar(Auditoria auditoria);
    }
}
