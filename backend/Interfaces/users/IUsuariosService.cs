using backend.Models.Users;
using UsuarioEntity = backend.Models.Users.Usuario;



namespace backend.Interfaces.Users

{
    public interface IUsuariosService
    {
        IEnumerable<UsuarioEntity> GetAllUsuarios();
        UsuarioEntity? GetUsuarioById(int id);
        UsuarioEntity? CreateUsuario(UsuarioEntity usuario);
        UsuarioEntity? UpdateUsuario(UsuarioEntity usuario);
        bool DeleteUsuario(int id);
        
    }
}