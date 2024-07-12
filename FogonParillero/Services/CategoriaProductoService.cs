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
        private readonly IAuditoriaInterface _auditoriaInterface;

        public CategoriaProductoService(ApplicationContext contexto, IImagenService imagenService, IAuditoriaInterface auditoriaInterface)
        {
            _contexto = contexto;
            _imagenService = imagenService;
            _auditoriaInterface = auditoriaInterface;
        }

        public async Task<CategoriaProducto> Actualizar(CategoriaProducto categoriaProducto)
        {
            _contexto.CategoriasProducto.Update(categoriaProducto);
            await _contexto.SaveChangesAsync();

            // Crear el registro de auditoría
            var auditoria = new Auditoria
            {
                TipoOperacion = "Actualizar",
                NombreTablaAfectada = "CategoriasProducto",
                IdRegistroAfectado = categoriaProducto.CategoriaId,
                DetallesCambio = "Actualización de categoría de producto",
                FechaHoraOperacion = DateTime.UtcNow,
            };

            await _auditoriaInterface.Registrar(auditoria);

            // Resto del código para actualizar la imagen
            var imagen = await _imagenService.SubirImagenPorRutaAsync(categoriaProducto.ImagenUrl, $"categoriaproducto{categoriaProducto.CategoriaId}");
            var imageUrl = $"https://fogonparillero.azurewebsites.net/api/Imagen/{imagen.filename}";
            categoriaProducto.ImagenUrl = imageUrl;
            _contexto.Update(categoriaProducto);
            await _contexto.SaveChangesAsync();
            return categoriaProducto;
        }

        public async Task<CategoriaProducto> Eliminar(CategoriaProducto categoriaProducto)
        {
            _contexto.CategoriasProducto.Remove(categoriaProducto);
            await _contexto.SaveChangesAsync();

            // Crear el registro de auditoría
            var auditoria = new Auditoria
            {
                TipoOperacion = "Eliminar",
                NombreTablaAfectada = "CategoriasProducto",
                IdRegistroAfectado = categoriaProducto.CategoriaId,
                DetallesCambio = "Eliminación de categoría de producto",
                FechaHoraOperacion = DateTime.UtcNow,
            };

            await _auditoriaInterface.Registrar(auditoria);

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

            // Crear el registro de auditoría
            var auditoria = new Auditoria
            {
                TipoOperacion = "Registrar",
                NombreTablaAfectada = "CategoriasProducto",
                IdRegistroAfectado = nuevoCategoriaProducto.CategoriaId,
                DetallesCambio = "Registro de nueva categoría de producto",
                FechaHoraOperacion = DateTime.UtcNow,
            };

            await _auditoriaInterface.Registrar(auditoria);

            // Resto del código para actualizar la imagen
            var imagen = await _imagenService.SubirImagenPorRutaAsync(ruta, $"categoriaproducto{nuevoCategoriaProducto.CategoriaId}");
            var imageUrl = $"https://fogonparillero.azurewebsites.net/api/Imagen/{imagen.filename}";
            nuevoCategoriaProducto.ImagenUrl = imageUrl;
            _contexto.Update(nuevoCategoriaProducto);
            await _contexto.SaveChangesAsync();
            return nuevoCategoriaProducto;
        }
    }
}
