using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FogonParillero.Data;
using FogonParillero.ViewModel;

namespace FogonParillero.Controllers
{
    public class MesaController : Controller
    {
        private readonly ApplicationContext _context;

        public MesaController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var mesas = _context.Mesa.ToList(); // Obtener las mesas desde la base de datos
            var viewModel = new ProductoViewModel
            {
                Mesa = mesas // Asignar las mesas al ViewModel
            };

            return View(viewModel); // Pasar el ViewModel a la vista
        }
    }
    
}
