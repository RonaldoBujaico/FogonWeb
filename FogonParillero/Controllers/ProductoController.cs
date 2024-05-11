using Microsoft.AspNetCore.Mvc;

namespace FogonParillero.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
