using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FogonParillero.Interfaces;
using FogonParillero.Models;

namespace FogonParillero.Services
{
    public class ImagenService : IImagenService
    {
        private readonly IConfiguration _configuration;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _blobContainerName;

        public ImagenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _blobServiceClient = new BlobServiceClient(_configuration.GetConnectionString("AzureBlobStorage"));
            _blobContainerName = "image";
        }

        public async Task<string> ObtenerImagenBase64Async(string filename)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
            BlobClient blobClient = containerClient.GetBlobClient(filename);

            Response<BlobProperties> properties = await blobClient.GetPropertiesAsync();

            byte[] bytes = new byte[properties.Value.ContentLength];

            using (var Stream = new MemoryStream(bytes))
            {
                await blobClient.DownloadToAsync(Stream);
            }

            return Convert.ToBase64String(bytes);
        }

        public async Task<Stream> ObtenerImagenStreamAsync(string filename)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
            BlobClient blobClient = containerClient.GetBlobClient(filename);
            var response = await blobClient.DownloadAsync();
            return response.Value.Content;
        }

        public async Task<Imagen> SubirImagenAsync(string imagen, string filename)
        {
            byte[] bytes = Convert.FromBase64String(imagen);

            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
            BlobClient blobClient = containerClient.GetBlobClient(filename);

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                await blobClient.UploadAsync(stream, true);
            }

            var result = new Imagen()
            {
                imagen = blobClient.Uri.ToString(),
                filename = filename
            };

            return result;
        }

        public async Task<Imagen> SubirImagenPorRutaAsync(string ruta, string filename)
        {
            string rutaCompleta = Path.Combine("wwwroot", "iconos", ruta);
            byte[] bytes = await File.ReadAllBytesAsync(rutaCompleta);

            var containerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
            var blobClient = containerClient.GetBlobClient(filename);

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                await blobClient.UploadAsync(stream, true);
            }

            var result = new Imagen()
            {
                imagen = blobClient.Uri.ToString(),
                filename = filename
            };

            return result;
        }
    }
}
