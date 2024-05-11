using FogonParillero.Data;
using FogonParillero.Interfaces;
using FogonParillero.Models;
using Microsoft.EntityFrameworkCore;

namespace FogonParillero.Services
{
    public class ProductoService : IProductoInterface
    {
        private readonly ApplicationContext _contexto;

        public async Task<Producto> Actualizar(Producto producto)
        {
            _contexto.Productos.Update(producto);
            await _contexto.SaveChangesAsync();
            return producto;
        }

        public async Task<Producto> Eliminar(Producto producto)
        {
            _contexto.Productos.Remove(producto);
            await _contexto.SaveChangesAsync();
            return producto;
        }

        public async Task<Producto> ObtenerPorId(int id)
        {
            return await _contexto.Productos.FindAsync(id);
        }

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
        {
            return await _contexto.Productos.ToListAsync();
        }

        public async Task<Producto> Registrar(Producto producto)
        {
            await _contexto.Productos.AddAsync(producto);
            await _contexto.SaveChangesAsync(); 
            return producto;
        }
    }
}
