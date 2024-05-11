using FogonParillero.Interfaces;
using FogonParillero.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FogonParillero.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioInterface _usuarioService;

        public LoginController(IUsuarioInterface usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string dni, string contraseña)
        {
            var autenticado = await _usuarioService.AutenticarUsuarioAsync(dni, contraseña);
            if (autenticado)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Credenciales incorrectas.");
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
