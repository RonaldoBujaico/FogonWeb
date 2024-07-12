using FogonParillero.Data;
using FogonParillero.Interfaces;
using FogonParillero.Models;
using Microsoft.EntityFrameworkCore;

namespace FogonParillero.Services
{
    public class DetalleInsumoService : IDetalleInsumoInterface
    {
        private readonly ApplicationContext _contexto;
        private readonly IAuditoriaInterface _auditoriaInterface;

        public DetalleInsumoService(ApplicationContext contexto, IAuditoriaInterface auditoriaInterface)
        {
            _contexto = contexto;
            _auditoriaInterface = auditoriaInterface;
        }

        public async Task<bool> AddDetalleInsumo(DetalleInsumo detalleInsumo)
        {
            await _contexto.DetalleInsumos.AddAsync(detalleInsumo);
            _contexto.SaveChanges();

            var auditoria = new Auditoria
            {
                TipoOperacion = "Registrar",
                NombreTablaAfectada = "DetalleInsumo",
                IdRegistroAfectado = detalleInsumo.DetalleInsumoId,
                DetallesCambio = "Registro de nuevo Detalle Insumo",
                FechaHoraOperacion = DateTime.UtcNow,
            };

            await _auditoriaInterface.Registrar(auditoria);

            return true;
        }

        public async Task<IEnumerable<DetalleInsumo>> ObtenerDetalleInsumo(int productoId)
        {
            return await _contexto.DetalleInsumos
                .Where(di => di.ProductoId == productoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<DetalleInsumo>> ObtenerDetallesInsumo()
        {
            return await _contexto.DetalleInsumos.ToListAsync();
        }
    }
}
