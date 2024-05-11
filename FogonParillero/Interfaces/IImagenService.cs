using FogonParillero.Models;

namespace FogonParillero.Interfaces
{
    public interface IImagenService
    {
        Task<Imagen> SubirImagenAsync(string imagen, string filename);
        Task<Imagen> SubirImagenPorRutaAsync(string imagen, string filename);
        Task<string> ObtenerImagenBase64Async(string filename);
        Task<Stream> ObtenerImagenStreamAsync(string filename);
    }
}
