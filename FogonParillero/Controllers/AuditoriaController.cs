using FogonParillero.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FogonParillero.Controllers
{
    public class AuditoriaController : Controller
    {
        private readonly IAuditoriaInterface _auditoriaService;

        public AuditoriaController(IAuditoriaInterface auditoriaService)
        {
            _auditoriaService = auditoriaService;
        }

        public async Task<IActionResult> Index()
        {
            var auditorias = await _auditoriaService.ObtenerTodosAsync();
            return View(auditorias);
        }
    }
}
