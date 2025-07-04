using backend.Models.Auth;
using backend.Interfaces.Usuario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
namespace entrantes.Controllers.Auth
{
    [ApiController]
    [Route("api/auth")]

    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;
        public AuthController(IAuthService AuthService, IConfiguration config)
        {
            _authService = AuthService;
            _config = config;
        }

       [HttpPost("login")]
public IActionResult Login([FromBody] LoginRequest loginRequest)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    if (string.IsNullOrEmpty(loginRequest.usuario) || string.IsNullOrEmpty(loginRequest.contraseña))
        return BadRequest("Usuario y contraseña son obligatorios.");

    var usuario = _authService.ValidarUsuario(loginRequest.usuario, loginRequest.contraseña);

    if (usuario == null)
        return Unauthorized("Credenciales inválidas.");

    // Crear claims seguros desde DB
    var claims = new[]
    {
         new Claim(ClaimTypes.Name, usuario.usuario),
        new Claim(ClaimTypes.Role, usuario.rol),
        new Claim("area", usuario.area)
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: _config["Jwt:Issuer"],
        audience: _config["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddHours(8),
        signingCredentials: creds
    );

    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

    return Ok(new
    {
        token = tokenString,
        usuario = usuario.usuario,
        Nombre = $"{usuario.Nombre_us} {usuario.Apellidos}",
        rol = usuario.rol,
        area = usuario.area
    });
}


    }
}
