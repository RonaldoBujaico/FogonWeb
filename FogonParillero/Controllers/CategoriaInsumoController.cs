using FogonParillero.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FogonParillero.Controllers
{

    public class CategoriaInsumoController : Controller
    {
        private readonly ICategoriaInsumoInterface _categoria;

        public CategoriaInsumoController(ICategoriaInsumoInterface categoria)
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
        public async Task<IActionResult> GuardarNuevaCategoria(string nombreCategoria)
        {
            await _categoria.Registrar(nombreCategoria);
            return RedirectToAction("Index");
        }








    }
}