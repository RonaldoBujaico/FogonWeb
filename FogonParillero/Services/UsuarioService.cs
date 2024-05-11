using FogonParillero.Data;
using FogonParillero.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FogonParillero.Services
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly ApplicationContext _contexto;

        public UsuarioService(ApplicationContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> AutenticarUsuarioAsync(string dni, string contraseña)
        {
            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Dni == dni && u.Contraseña == contraseña);
            return usuario != null;
        }
    }
}
