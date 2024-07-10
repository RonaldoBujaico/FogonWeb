using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using FogonParillero.Data;
using FogonParillero.ViewModel;

namespace FogonParillero.Controllers
{
    public class VentasController : Controller
    {
        private readonly ApplicationContext _context;

        public VentasController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var pedidos = _context.Pedido
                .Include(p => p.Mesa) // Incluir la relación con la mesa si es necesario
                .ToList();

            var viewModel = new ProductoViewModel
            {
                Pedidos = pedidos // Asignar los pedidos al ViewModel
            };

            return View(viewModel); // Pasar el ViewModel a la vista
        }

    }
}
