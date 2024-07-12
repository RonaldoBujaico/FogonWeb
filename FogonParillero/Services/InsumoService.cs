using FogonParillero.Data;
using FogonParillero.Interfaces;
using FogonParillero.Models;
using Microsoft.EntityFrameworkCore;

namespace FogonParillero.Services
{
    public class InsumoService : IInsumoInterface
    {
        private readonly ApplicationContext _contexto;
        private readonly IImagenService _imagenService;
        private readonly IAuditoriaInterface _auditoriaInterface;

        public InsumoService(ApplicationContext contexto, IImagenService imagenService, IAuditoriaInterface auditoriaInterface)
        {
            _contexto = contexto;
            _imagenService = imagenService;
            _auditoriaInterface = auditoriaInterface;
        }

        public async Task<Insumo> Actualizar(Insumo Insumo)
        {
            var base64 = Insumo.Imagen;
            Insumo.Imagen = "";

            _contexto.Insumos.Update(Insumo);
            await _contexto.SaveChangesAsync();

            if (base64 != null)
            {
                var imagen = await _imagenService.SubirImagenAsync(base64, $"insumo{Insumo.InsumoId}");
                var imagenUrl = $"https://fogonparillero.azurewebsites.net/api/Imagen/{imagen.filename}";
                Insumo.Imagen = imagenUrl;
                _contexto.Update(Insumo);
                await _contexto.SaveChangesAsync();

                var auditoria = new Auditoria
                {
                    TipoOperacion = "Actualizar",
                    NombreTablaAfectada = "Insumo",
                    IdRegistroAfectado = Insumo.CategoriaInsumoId,
                    DetallesCambio = "Actualización de insumo",
                    FechaHoraOperacion = DateTime.UtcNow,
                };

                await _auditoriaInterface.Registrar(auditoria);
            }
            return Insumo;
        }

        public async Task<Insumo> Eliminar(Insumo Insumo)
        {
            _contexto.Insumos.Remove(Insumo);
            await _contexto.SaveChangesAsync();

            var auditoria = new Auditoria
            {
                TipoOperacion = "Eliminar",
                NombreTablaAfectada = "Insumo",
                IdRegistroAfectado = Insumo.CategoriaInsumoId,
                DetallesCambio = "Eliminación de insumo",
                FechaHoraOperacion = DateTime.UtcNow,
            };

            await _auditoriaInterface.Registrar(auditoria);

            return Insumo;
        }

        public async Task<Insumo> ObtenerPorId(int id)
        {
            return await _contexto.Insumos.FindAsync(id);

        }

        public async Task<IEnumerable<Insumo>> ObtenerTodosAsync()
        {
            return await _contexto.Insumos.ToListAsync();
        }

        public async Task<Insumo> Registrar(string nombre, string descrip, int cant, string img, int idcatinsu, int unidad)
        {
            var nuevoInsumo = new Insumo
            {
                Nombre = nombre,
                Descripcion = descrip,
                Cantidad = cant,
                Imagen = "",
                CategoriaInsumoId = idcatinsu,
                UnidadId = unidad
            };
            await _contexto.Insumos.AddAsync(nuevoInsumo);
            await _contexto.SaveChangesAsync();

            var auditoria = new Auditoria
            {
                TipoOperacion = "Registrar",
                NombreTablaAfectada = "Insumo",
                IdRegistroAfectado = nuevoInsumo.InsumoId,
                DetallesCambio = "Registro de nuevo insumo",
                FechaHoraOperacion = DateTime.UtcNow,
            };

            await _auditoriaInterface.Registrar(auditoria);

            if (img != null)
            {
                var imagen = await _imagenService.SubirImagenAsync(img, $"insumo{nuevoInsumo.InsumoId}");
                var imagenUrl = $"https://fogonparillero.azurewebsites.net/api/Imagen/{imagen.filename}";
                nuevoInsumo.Imagen = imagenUrl;
                _contexto.Update(nuevoInsumo);
                await _contexto.SaveChangesAsync();
            }
            return nuevoInsumo;

        }
    }   
}
