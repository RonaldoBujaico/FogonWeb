using FogonParillero.Models;
using FogonParillero.Interfaces;
using FogonParillero.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FogonParillero.Controllers
{
    public class DetalleInsumoController : Controller
    {
        private readonly IProductoInterface _producto;
        private readonly IInsumoInterface _insumo;
        private readonly IDetalleInsumoInterface _detalleInsumo;

        public DetalleInsumoController(IProductoInterface producto, IInsumoInterface insumo, IDetalleInsumoInterface detalleInsumo)
        {
            _producto = producto;
            _insumo = insumo;
            _detalleInsumo = detalleInsumo;
        }

        public async Task<IActionResult> Index()
        {
            var model = new DetalleInsumoViewModel
            {
                Productos = await _producto.ObtenerTodosAsync(),
                Insumos = await _insumo.ObtenerTodosAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(string productoId, List<DetalleInsumo> detalleInsumos)
        {
            // Debugging logs
            Console.WriteLine("ProductoId: " + productoId);
            if (detalleInsumos != null)
            {
                foreach (var detalle in detalleInsumos)
                {
                    Console.WriteLine("InsumoId: " + detalle.InsumoId + ", Cantidad: " + detalle.Cantidad);
                }
            }
            else
            {
                Console.WriteLine("No se recibieron detalles de insumo");
            }

            if (!string.IsNullOrEmpty(productoId) && detalleInsumos != null && detalleInsumos.Any())
            {
                foreach (var detalle in detalleInsumos)
                {
                    detalle.ProductoId = int.Parse(productoId);
                    await _detalleInsumo.AddDetalleInsumo(detalle);
                }
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
