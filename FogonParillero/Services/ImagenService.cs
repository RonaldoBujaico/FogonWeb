using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FogonParillero.Interfaces;
using FogonParillero.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Png;


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
        public async Task<Imagen> SubirImagenAsync(string imagenBase64, string filename)
        {
            BlobClient blobClient;

            // Convertir la imagen base64 en bytes
            byte[] bytes = Convert.FromBase64String(imagenBase64);

            // Crear una imagen a partir de los bytes
            using (Image image = Image.Load(bytes))
            {
                // Redimensionar la imagen a 512x512
                image.Mutate(x => x.Resize(512, 512));

                // Guardar la imagen redimensionada en un nuevo MemoryStream
                using (MemoryStream resizedStream = new MemoryStream())
                {
                    image.Save(resizedStream, new PngEncoder());

                    // Subir la imagen redimensionada al Blob Storage
                    BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
                    blobClient = containerClient.GetBlobClient(filename);

                    resizedStream.Position = 0; // Reiniciar la posición del stream
                    await blobClient.UploadAsync(resizedStream, true);
                }
            }

            // Crear un objeto Imagen con la URL del Blob Storage
            var result = new Imagen()
            {
                imagen = blobClient.Uri.ToString(),
                filename = filename
            };

            return result;
        }

        public async Task<Imagen> SubirImagenPorRutaAsync(string ruta, string filename)
        {
            BlobClient blobClient;
            string rutaCompleta = Path.Combine("wwwroot", "iconos", ruta);

            byte[] bytes = await File.ReadAllBytesAsync(rutaCompleta);

            using (Image image = Image.Load(bytes))
            {
                image.Mutate(x => x.Resize(256, 256));

                using (MemoryStream resizedStream = new MemoryStream())
                {
                    image.Save(resizedStream, new PngEncoder());

                    var containerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
                    blobClient = containerClient.GetBlobClient(filename);

                    resizedStream.Position = 0;
                    await blobClient.UploadAsync(resizedStream, true);
                }
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
