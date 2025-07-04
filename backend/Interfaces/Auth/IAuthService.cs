using backend.Models.Auth;
using backend.Models.Users;
using UsuarioEntity = backend.Models.Users.Usuario;

namespace backend.Interfaces.Usuario
{
    public interface IAuthService
    {
        UsuarioEntity? ValidarUsuario(string usuario, string contraseña);
    }
}
