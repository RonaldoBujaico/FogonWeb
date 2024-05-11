using FogonParillero.Data;
using FogonParillero.Interfaces;
using FogonParillero.Models;
using Microsoft.EntityFrameworkCore;

namespace FogonParillero.Services
{
    public class CategoriaProductoService : ICategoriaProductoInterface
    {
        private readonly ApplicationContext _contexto;
        private readonly IImagenService _imagenService;

        public CategoriaProductoService(ApplicationContext contexto, IImagenService imagenService)
        {
            _contexto = contexto;
            _imagenService = imagenService;
        }

        public async Task<CategoriaProducto> Actualizar(CategoriaProducto categoriaProducto)
        {
            _contexto.CategoriasProducto.Update(categoriaProducto);
            await _contexto.SaveChangesAsync();
            return categoriaProducto;
        }

        public async Task<CategoriaProducto> Eliminar(CategoriaProducto categoriaProducto)
        {
            _contexto.CategoriasProducto.Remove(categoriaProducto);
            await _contexto.SaveChangesAsync();
            return categoriaProducto;
        }

        public async Task<CategoriaProducto> ObtenerPorId(int id)
        {
            return await _contexto.CategoriasProducto.FindAsync(id);
        }

        public async Task<IEnumerable<CategoriaProducto>> ObtenerTodosAsync()
        {
            return await _contexto.CategoriasProducto.ToListAsync();
        }

        public async Task<CategoriaProducto> Registrar(string nombre, string ruta)
        {
            var nuevoCategoriaProducto = new CategoriaProducto
            {
                Nombre = nombre,
                ImagenUrl = ruta
            };

            await _contexto.CategoriasProducto.AddAsync(nuevoCategoriaProducto);
            await _contexto.SaveChangesAsync();

            var imagen = await _imagenService.SubirImagenPorRutaAsync(ruta, $"categoriaproducto{nuevoCategoriaProducto.CategoriaId}");
            var imageUrl = $"https://localhost:7122/api/Imagen/{imagen.filename}";
            nuevoCategoriaProducto.ImagenUrl = imageUrl;
            _contexto.Update(nuevoCategoriaProducto);
            await _contexto.SaveChangesAsync();
            return nuevoCategoriaProducto;
        }
    }
}
