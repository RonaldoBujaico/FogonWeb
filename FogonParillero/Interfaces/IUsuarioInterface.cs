using FogonParillero.Models;

namespace FogonParillero.Interfaces
{
    public interface IUsuarioInterface
    {
        Task<Usuario> AutenticarUsuarioAsync(string dni, string contraseña);
    }
}
