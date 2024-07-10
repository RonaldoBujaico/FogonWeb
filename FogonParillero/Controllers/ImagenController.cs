using FogonParillero.Interfaces;
using FogonParillero.Models;
using Microsoft.AspNetCore.Mvc;

namespace FogonParillero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenController : ControllerBase
    {
        private readonly IImagenService _imagenService;

        public ImagenController(IImagenService imagenService)
        {
            _imagenService = imagenService;
        }

        [HttpPost("base64")]
        public async Task<IActionResult> SubirImagenAsync(Imagen imagen)
        {
            var url = await _imagenService.SubirImagenAsync(imagen.imagen, imagen.filename);
            url.imagen = $"https://fogonparillero.azurewebsites.net/api/Imagen/{url.filename}";

            return Ok(url);
        }

        [HttpGet("getimagebase64/{filename}")]
        public async Task<IActionResult> ObtenerImagenAsync(string filename)
        {
            var imagenModel = await _imagenService.ObtenerImagenBase64Async(filename);
            return Ok(imagenModel);
        }

        [HttpGet("{filename}")]
        public async Task<IActionResult> ObtenerImagenStreamAsync(string filename)
        {
            var stream = await _imagenService.ObtenerImagenStreamAsync(filename);
            return File(stream, "image/png");
        }

        [HttpPost("ruta")]
        public async Task<IActionResult> SubirImagenPorRutaAsync(Imagen imagen)
        {
            var url = await _imagenService.SubirImagenPorRutaAsync(imagen.imagen, imagen.filename);
            url.imagen = $"https://fogonparillero.azurewebsites.net/api/Imagen/{url.filename}";

            return Ok(url);
        }
    }
}
