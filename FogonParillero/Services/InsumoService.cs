using FogonParillero.Data;
using FogonParillero.Interfaces;
using FogonParillero.Models;
using Microsoft.EntityFrameworkCore;

namespace FogonParillero.Services
{
    public class InsumoService : IInsumoInterface
    {
        private readonly ApplicationContext _contexto;

        public InsumoService(ApplicationContext contexto)
        {
            _contexto = contexto;
        }
        public async Task<IEnumerable<Insumo>> ObtenerTodosAsync()
        {
            return await _contexto.Insumos.ToListAsync();
        }
    }   
}
