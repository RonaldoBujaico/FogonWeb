using FogonParillero.Data;
using FogonParillero.Interfaces;
using FogonParillero.Models;
using Microsoft.EntityFrameworkCore;

namespace FogonParillero.Services
{
    public class CategoriaInsumoService : ICategoriaInsumoInterface
    {
        private readonly ApplicationContext _contexto;
        private readonly IAuditoriaInterface _auditoriaInterface;


        public CategoriaInsumoService(ApplicationContext contexto, IAuditoriaInterface auditoriaInterface)
        {
            _contexto = contexto;
            _auditoriaInterface = auditoriaInterface;
        }

        public async Task<CategoriaInsumo> Actualizar(CategoriaInsumo categoriaInsumo)
        {
            _contexto.CategoriasInsumo.Update(categoriaInsumo);
            await _contexto.SaveChangesAsync();

            var auditoria = new Auditoria
            {
                TipoOperacion = "Actualizar",
                NombreTablaAfectada = "CategoriaInsumo",
                IdRegistroAfectado = categoriaInsumo.CategoriaId,
                DetallesCambio = "Actualización de categoría de insumo",
                FechaHoraOperacion = DateTime.UtcNow,
            };

            await _auditoriaInterface.Registrar(auditoria);
            return categoriaInsumo;
        }

        public async Task<CategoriaInsumo> Eliminar(CategoriaInsumo categoriaInsumo)
        {
            _contexto.CategoriasInsumo.Remove(categoriaInsumo);

            var auditoria = new Auditoria
            {
                TipoOperacion = "Eliminar",
                NombreTablaAfectada = "CategoriaInsumo",
                IdRegistroAfectado = categoriaInsumo.CategoriaId,
                DetallesCambio = "Eliminación de categoría de insumo",
                FechaHoraOperacion = DateTime.UtcNow,
            };

            await _auditoriaInterface.Registrar(auditoria);

            await _contexto.SaveChangesAsync();
            return categoriaInsumo;
        }

        public async Task<CategoriaInsumo> ObtenerPorId(int id)
        {
            return await _contexto.CategoriasInsumo.FindAsync(id);
        }

        public async Task<IEnumerable<CategoriaInsumo>> ObtenerTodosAsync()
        {
            return await _contexto.CategoriasInsumo.ToListAsync();
        }

        public async Task<CategoriaInsumo> Registrar(string nombre)
        {
            var nuevoCategoriaInsumo = new CategoriaInsumo
            {
                Nombre = nombre
            };

            await _contexto.CategoriasInsumo.AddAsync(nuevoCategoriaInsumo);
            await _contexto.SaveChangesAsync();

            var auditoria = new Auditoria
            {
                TipoOperacion = "Registrar",
                NombreTablaAfectada = "CategoriaInsumo",
                IdRegistroAfectado = nuevoCategoriaInsumo.CategoriaId,
                DetallesCambio = "Registro de nueva categoría de insumo",
                FechaHoraOperacion = DateTime.UtcNow,
            };

            await _auditoriaInterface.Registrar(auditoria);


            return nuevoCategoriaInsumo;
        }

    }
}