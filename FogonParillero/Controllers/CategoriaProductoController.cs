using FogonParillero.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FogonParillero.Controllers
{
    public class CategoriaProductoController : Controller
    {
        private readonly ICategoriaProductoInterface _categoria;

        public CategoriaProductoController(ICategoriaProductoInterface categoria)
        {
            _categoria = categoria;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await _categoria.ObtenerTodosAsync();
            ViewData["ModalAbierto"] = false;
            return View(categorias);
        }

        [HttpGet]
        public async Task<IActionResult> Registrar()
        {
            ViewData["ModalAbierto"] = true;
            var categorias = await _categoria.ObtenerTodosAsync();
            return View("Index", categorias);
        }

        [HttpPost]
        public IActionResult CerrarModal()
        {
            ViewData["ModalAbierto"] = false;
            // Redireccionar a la ruta /categoriaproducto
            return RedirectToAction("Index", "CategoriaProducto");
        }

        [HttpPost]
        public async Task<IActionResult> GuardarNuevaCategoria(string nombreCategoria, string iconoCategoria)
        {
            await _categoria.Registrar(nombreCategoria, iconoCategoria);
            return RedirectToAction("Index");
        }
    }
}
