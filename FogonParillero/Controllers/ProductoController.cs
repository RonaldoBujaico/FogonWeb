using FogonParillero.Interfaces;
using FogonParillero.Models;
using FogonParillero.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FogonParillero.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoInterface _producto;
        private readonly ICategoriaProductoInterface _categoriaProducto;
        private readonly IInsumoInterface _insumo;

        public ProductoController(IProductoInterface producto, ICategoriaProductoInterface categoriaProducto, IInsumoInterface insumo)
        {
            _producto = producto;
            _categoriaProducto = categoriaProducto;
            _insumo = insumo;
        }

        public async Task<IActionResult> Index()
        {
            var productoViewModel = new ProductoViewModel
            {
                Productos = await _producto.ObtenerTodosAsync(),
                Categorias = await _categoriaProducto.ObtenerTodosAsync(),
                Insumos = await _insumo.ObtenerTodosAsync(),
            };

            return View(productoViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarNuevoProducto(string nombreProducto, string? descripcion, decimal precioVenta, string imagenUrl, int categoriaProductoId)
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
        public async Task<IActionResult> Eliminar(int id)
        {
            var producto = await _producto.ObtenerPorId(id);
            await _producto.Eliminar(producto);
            return RedirectToAction("Index");
        }
    }
}
