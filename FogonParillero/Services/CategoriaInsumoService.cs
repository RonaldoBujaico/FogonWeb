using FogonParillero.Data;
using FogonParillero.Interfaces;
using FogonParillero.Models;
using Microsoft.EntityFrameworkCore;

namespace FogonParillero.Services
{
    public class CategoriaInsumoService : ICategoriaInsumoInterface
    {
        private readonly ApplicationContext _contexto;


        public CategoriaInsumoService(ApplicationContext contexto)
        {
            _contexto = contexto;

        }

        public async Task<CategoriaInsumo> Actualizar(CategoriaInsumo categoriaInsumo)
        {
            _contexto.CategoriasInsumo.Update(categoriaInsumo);
            await _contexto.SaveChangesAsync();
            return categoriaInsumo;
        }

        public async Task<CategoriaInsumo> Eliminar(CategoriaInsumo categoriaInsumo)
        {
            _contexto.CategoriasInsumo.Remove(categoriaInsumo);
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


            return nuevoCategoriaInsumo;
        }

    }
}