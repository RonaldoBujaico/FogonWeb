using FogonParillero.Data;
using FogonParillero.Interfaces;
using FogonParillero.Models;
using Microsoft.EntityFrameworkCore;

namespace FogonParillero.Services
{
    public class DetalleInsumoService : IDetalleInsumoInterface
    {
        private readonly ApplicationContext _contexto;

        public DetalleInsumoService(ApplicationContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> AddDetalleInsumo(DetalleInsumo detalleInsumo)
        {
            await _contexto.DetalleInsumos.AddAsync(detalleInsumo);
            _contexto.SaveChanges();
            return true;
        }
    }
}
