using FogonParillero.Data;
using FogonParillero.Interfaces;
using FogonParillero.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FogonParillero.Services
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly ApplicationContext _contexto;

        public UsuarioService(ApplicationContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<Usuario> AutenticarUsuarioAsync(string dni, string contraseña)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Dni == dni && u.Contraseña == contraseña);
        }
    }
}
