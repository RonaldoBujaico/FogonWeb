using FogonParillero.Data;
using FogonParillero.Interfaces;
using FogonParillero.Models;
using Microsoft.EntityFrameworkCore;

namespace FogonParillero.Services
{
    public class UnidadService : IUnidadInterface
    {
        private readonly ApplicationContext _contexto;
     

        public UnidadService(ApplicationContext contexto)
        {
            _contexto = contexto;
           
        }
        public async Task<IEnumerable<Unidad>> ObtenerTodosAsync()
        {
            return await _contexto.Unidades.ToListAsync(); 
        }
    }
}
