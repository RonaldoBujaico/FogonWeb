using FogonParillero.Data;
using FogonParillero.Interfaces;
using FogonParillero.Models;
using Microsoft.EntityFrameworkCore;

namespace FogonParillero.Services
{
    public class ProductoService : IProductoInterface
    {
        private readonly ApplicationContext _contexto;
        private readonly IImagenService _imagenService;
        private readonly IAuditoriaInterface _auditoriaInterface;

        public ProductoService(ApplicationContext contexto, IImagenService imagenService, IAuditoriaInterface auditoriaInterface)
        {
            _contexto = contexto;
            _imagenService = imagenService;
            _auditoriaInterface = auditoriaInterface;
        }

        public async Task<Producto> Actualizar(Producto producto)
        {
            var base64 = producto.ImagenUrl;
            producto.ImagenUrl = "";

            _contexto.Productos.Update(producto);
            await _contexto.SaveChangesAsync();

            var auditoria = new Auditoria
            {
                TipoOperacion = "Actualizar",
                NombreTablaAfectada = "Producto",
                IdRegistroAfectado = producto.ProductoId,
                DetallesCambio = "Actualización de producto",
                FechaHoraOperacion = DateTime.UtcNow,
            };

            await _auditoriaInterface.Registrar(auditoria);

            if (base64 != null)
            {
                var imagen = await _imagenService.SubirImagenAsync(base64, $"producto{producto.ProductoId}");
                var imagenUrl = $"https://fogonparillero.azurewebsites.net/api/Imagen/{imagen.filename}";
                producto.ImagenUrl = imagenUrl;
                _contexto.Update(producto);
                await _contexto.SaveChangesAsync();
            }
            return producto;
        }

        public async Task<Producto> Eliminar(Producto producto)
        {
            _contexto.Productos.Remove(producto);
            await _contexto.SaveChangesAsync();

            var auditoria = new Auditoria
            {
                TipoOperacion = "Eliminar",
                NombreTablaAfectada = "Producto",
                IdRegistroAfectado = producto.CategoriaId,
                DetallesCambio = "Eliminación de producto",
                FechaHoraOperacion = DateTime.UtcNow,
            };

            await _auditoriaInterface.Registrar(auditoria);

            return producto;
        }

        public async Task<Producto> ObtenerPorId(int id)
        {
            return await _contexto.Productos.FindAsync(id);
        }

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
        {
            var productos =  await _contexto.Productos.ToListAsync();
            return productos.Any() ? productos : null;
        }

        public async Task<Producto> Registrar(Producto producto)
        {
            var nuevoProducto = new Producto
            {
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                ImagenUrl = "",
                CategoriaId = producto.CategoriaId,
            };
            await _contexto.Productos.AddAsync(nuevoProducto);
            await _contexto.SaveChangesAsync();

            var auditoria = new Auditoria
            {
                TipoOperacion = "Registrar",
                NombreTablaAfectada = "Producto",
                IdRegistroAfectado = producto.ProductoId,
                DetallesCambio = "Registro de nuevo producto",
                FechaHoraOperacion = DateTime.UtcNow,
            };

            await _auditoriaInterface.Registrar(auditoria);

            if (producto.ImagenUrl != null)
            {
                var imagen = await _imagenService.SubirImagenAsync(producto.ImagenUrl, $"producto{nuevoProducto.ProductoId}");
                var imagenUrl = $"https://fogonparillero.azurewebsites.net/api/Imagen/{imagen.filename}";
                nuevoProducto.ImagenUrl = imagenUrl;
                _contexto.Update(nuevoProducto);
                await _contexto.SaveChangesAsync();
            }
            return nuevoProducto;
        }
    }
}
