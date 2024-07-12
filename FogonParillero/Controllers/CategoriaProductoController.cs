using FogonParillero.Interfaces;
using FogonParillero.Models;
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
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            var categorias = await _categoria.ObtenerTodosAsync();
            ViewData["ModalAbierto"] = false;
            return View(categorias);
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

        [HttpPost]
        public async Task<IActionResult> ActualizarCategoria(int id, string nombreCategoria, string iconoCategoria)
        {
            var categoriaExistente = await _categoria.ObtenerPorId(id);

            categoriaExistente.Nombre = nombreCategoria;
            categoriaExistente.ImagenUrl = iconoCategoria;

            await _categoria.Actualizar(categoriaExistente);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var categoriaProducto = await _categoria.ObtenerPorId(id);
            await _categoria.Eliminar(categoriaProducto);
            return RedirectToAction("Index");
        }
    }
}
