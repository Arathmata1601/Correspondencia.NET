using backend.Db;
using backend.Models.Auth;
using BCrypt.Net; // Asegúrate de tener la biblioteca BCrypt.Net instalada
using UsuarioEntity = backend.Models.Users.Usuario;

using backend.Interfaces.Usuario;

namespace backend.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

       public UsuarioEntity? ValidarUsuario(string usuario, string contraseña)
{
    var user = _context.Usuarios.FirstOrDefault(u => u.usuario == usuario);
    if (user == null) return null;

    // Usando BCrypt para comparar contraseña
    bool isValid = BCrypt.Net.BCrypt.Verify(contraseña, user.contraseña);
    return isValid ? user : null;
}

    }
}
