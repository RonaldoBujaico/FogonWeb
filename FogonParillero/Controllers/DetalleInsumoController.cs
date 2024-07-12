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
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            var model = new DetalleInsumoViewModel
            {
                Productos = await _producto.ObtenerTodosAsync(),
                Insumos = await _insumo.ObtenerTodosAsync(),
                DetallesInsumosPorProducto = new Dictionary<int, IEnumerable<DetalleInsumo>>()
            };

            foreach (var producto in model.Productos)
            {
                var detallesInsumos = await _detalleInsumo.ObtenerDetalleInsumo(producto.ProductoId);
                model.DetallesInsumosPorProducto.Add(producto.ProductoId, detallesInsumos);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(string productoId, List<DetalleInsumo> detalleInsumos)
        {
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
