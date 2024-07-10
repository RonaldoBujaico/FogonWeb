using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FogonParillero.Data; // Asegúrate de tener el namespace correcto para ApplicationContext
using FogonParillero.Models; // Asegúrate de tener el namespace correcto para Producto
using FogonParillero.ViewModel;

namespace FogonParillero.Controllers
{
    public class MesaDetalleController : Controller
    {
        private readonly ApplicationContext _context;

        public MesaDetalleController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var productos = _context.Productos.ToList(); // Obtener todos los productos desde la base de datos

            var viewModel = new ProductoViewModel
            {
                Productos = productos // Asignar los productos al ViewModel
            };

            return View(viewModel); // Pasar el ViewModel a la vista
        }
    }
}
