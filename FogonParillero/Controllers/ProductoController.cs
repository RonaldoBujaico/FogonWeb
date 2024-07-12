using FogonParillero.Interfaces;
using FogonParillero.Models;
using FogonParillero.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FogonParillero.Controllers
{
    public class ProductoController : Controller
    {
        public int ProductoSeleccionadoId { get; set; }
        private readonly IProductoInterface _producto;
        private readonly ICategoriaProductoInterface _categoriaProducto;
        private readonly IInsumoInterface _insumo;

        public ProductoController(IProductoInterface producto, ICategoriaProductoInterface categoriaProducto, IInsumoInterface insumo)
        {
            _producto = producto;
            _categoriaProducto = categoriaProducto;
            _insumo = insumo;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            var productoViewModel = new ProductoViewModel
            {
                Productos = await _producto.ObtenerTodosAsync(),
                Categorias = await _categoriaProducto.ObtenerTodosAsync(),
                Insumos = await _insumo.ObtenerTodosAsync(),
                Producto = new Producto()
                {
                    ProductoId = id ?? 0
                },
            };

            return View(productoViewModel);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            var producto = await _producto.ObtenerPorId(id);
            if (producto == null)
            {
                return NotFound();
            }

            string imagenBase64 = await ConvertirImagenUrlABase64(producto.ImagenUrl);
            producto.ImagenUrl = imagenBase64;

            return Json(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(string nombreProducto, string? descripcion, decimal precioVenta, string imagenUrl, int categoriaProductoId)
        {
            var nuevoProducto = new Producto{
                Nombre = nombreProducto,
                Descripcion = descripcion,
                Precio = precioVenta,
                ImagenUrl = imagenUrl,
                CategoriaId = categoriaProductoId
            };

            await _producto.Registrar(nuevoProducto);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Actualizar(int productoIdEditar, string nombreProducto, string? descripcion, decimal precioVenta, string imagenUrl, int categoriaProductoId)
        {
            var producto = await _producto.ObtenerPorId(productoIdEditar);
            producto.Nombre = nombreProducto;
            producto.Descripcion = descripcion;
            producto.Precio = precioVenta;
            producto.ImagenUrl = imagenUrl;
            producto.CategoriaId = categoriaProductoId;

            await _producto.Actualizar(producto);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var producto = await _producto.ObtenerPorId(id);
            await _producto.Eliminar(producto);
            return RedirectToAction("Index");
        }

        private async Task<string> ConvertirImagenUrlABase64(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
                    return Convert.ToBase64String(imageBytes);
                }
                return null;
            }
        }
    }
}
