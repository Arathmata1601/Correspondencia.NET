using backend.Models.Users;
using backend.Interfaces.Users;
using backend.Db;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Cryptography;
using BCrypt.Net; // Asegúrate de tener la biblioteca BCrypt.Net instalada

namespace backend.Services.Users
{
    public class UsuariosService : IUsuariosService
    {
        private readonly AppDbContext _context;

        public UsuariosService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Usuario> GetAllUsuarios()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario? GetUsuarioById(int id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.id_us == id);
        }

        public Usuario? CreateUsuario(Usuario usuario)
        {
            // Hashear la contraseña antes de guardar
            usuario.contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.contraseña);

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }

        public Usuario? UpdateUsuario(Usuario usuario)
        {
            var usuarioExistente = _context.Usuarios.FirstOrDefault(u => u.id_us == usuario.id_us);
            if (usuarioExistente == null)
                return null;

            usuarioExistente.Nombre_us = usuario.Nombre_us;
            usuarioExistente.Apellidos = usuario.Apellidos;
            usuarioExistente.usuario = usuario.usuario;
            usuarioExistente.area = usuario.area;
            usuarioExistente.rol = usuario.rol;

            // 👇 Hashear SI y solo SI la contraseña cambió (opcional)
            if (!string.IsNullOrWhiteSpace(usuario.contraseña) &&
                !BCrypt.Net.BCrypt.Verify(usuario.contraseña, usuarioExistente.contraseña))
            {
                usuarioExistente.contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.contraseña);
            }

            _context.Usuarios.Update(usuarioExistente);
            _context.SaveChanges();

            return usuarioExistente;
        }

        public bool DeleteUsuario(int id)
        {
            var usuario = GetUsuarioById(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return true;
        }
    }
}