using FogonParillero.Data;
using FogonParillero.Interfaces;
using FogonParillero.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FogonParillero.Services
{
    public class AuditoriaService : IAuditoriaInterface
    {
        private readonly ApplicationContext _contexto;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditoriaService(ApplicationContext contexto, IHttpContextAccessor httpContextAccessor)
        {
            _contexto = contexto;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Auditoria> ObtenerPorId(int id)
        {
            return await _contexto.Auditoria
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Auditoria>> ObtenerTodosAsync()
        {
            return await _contexto.Auditoria
                .Include(a => a.Usuario)
                .ToListAsync();
        }

        public async Task<Auditoria> Registrar(Auditoria auditoria)
        {
            // Obtener el usuario actual
            var usuarioId = ObtenerUsuarioId();

            // Asignar el usuario a la auditoría
            auditoria.UsuarioId = usuarioId;

            await _contexto.Auditoria.AddAsync(auditoria);
            await _contexto.SaveChangesAsync();
            return auditoria;
        }

        private int ObtenerUsuarioId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return 0;
        }
    }
}
