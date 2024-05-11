namespace FogonParillero.Interfaces
{
    public interface IUsuarioInterface
    {
        Task<bool> AutenticarUsuarioAsync(string dni, string contraseña);
    }
}
