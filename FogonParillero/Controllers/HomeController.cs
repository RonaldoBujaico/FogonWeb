using Microsoft.AspNetCore.Mvc;

namespace FogonParillero.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
