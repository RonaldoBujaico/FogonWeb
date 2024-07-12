using FogonParillero.Interfaces;
using FogonParillero.Models;
using FogonParillero.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FogonParillero.Controllers
{
    public class InsumoController : Controller
    {
        public int InsumoSeleccionadoId { get; set; }
        private readonly IInsumoInterface _insumo;
        private readonly ICategoriaInsumoInterface _categoriaInsumo;
        private readonly IUnidadInterface _unidad;

        public InsumoController(IInsumoInterface insumo, ICategoriaInsumoInterface categoriainsumo, IUnidadInterface unidad)
        {
            _insumo = insumo;
            _categoriaInsumo = categoriainsumo;
            _unidad = unidad;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            var insumoViewModel = new InsumoViewModel
            {
                Insumos = await _insumo.ObtenerTodosAsync(),
                Categorias = await _categoriaInsumo.ObtenerTodosAsync(),
                Unidades = await _unidad.ObtenerTodosAsync(),
                Insumo = new Insumo()
                {
                    InsumoId = id ?? 0
                },
            };

            return View(insumoViewModel);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            var insumo = await _insumo.ObtenerPorId(id);
            if (insumo == null)
            {
                return NotFound();
            }

            string imagenBase64 = await ConvertirImagenUrlABase64(insumo.Imagen);
            insumo.Imagen = imagenBase64;

            return Json(insumo);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(string nombreinsumo, string? descripcion, int cantidad, string imagenUrl, int categoriaInsumoId, int UnidadId)
        {
           
            await _insumo.Registrar(nombreinsumo,descripcion,cantidad,imagenUrl,categoriaInsumoId,UnidadId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(int insumoIdEditar, string nombreinsumo, string? descripcion, int cantidad, string imagenUrl, int categoriaInsumoId, int UnidadId)
        {
            var insumo = await _insumo.ObtenerPorId(insumoIdEditar);
            insumo.Nombre = nombreinsumo;
            insumo.Descripcion = descripcion;
            insumo.Cantidad = cantidad;
            insumo.Imagen = imagenUrl;
            insumo.CategoriaInsumoId = categoriaInsumoId;
            insumo.UnidadId = UnidadId;

            await _insumo.Actualizar(insumo);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var insumo = await _insumo.ObtenerPorId(id);
            await _insumo.Eliminar(insumo);
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
