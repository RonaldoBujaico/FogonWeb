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
            string hashedContraseña = GenerarHashSHA512(contraseña);
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Dni == dni && u.Contraseña == hashedContraseña);
        }

        private string GenerarHashSHA512(string input)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                byte[] hashBytes = sha512.ComputeHash(inputBytes);

                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    stringBuilder.Append(b.ToString("X2")); 
                }
                return stringBuilder.ToString();
            }
        }
    }
}
