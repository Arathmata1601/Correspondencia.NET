namespace backend.Controllers.Users;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend.Models.Users;
using backend.Interfaces.Users;

[ApiController]
[Route("api/")]
public class UsuariosController : Controller
{
    private readonly IUsuariosService _usuariosService;

    public UsuariosController(IUsuariosService usuariosService)
    {
        _usuariosService = usuariosService;
    }


    [HttpGet("usuarios")]
    [Authorize(Roles = "root")]
    [ProducesResponseType(typeof(IEnumerable<Usuario>), 200)]
    public IActionResult GetUsuarios()
    {
        var usuarios = _usuariosService.GetAllUsuarios();
        return Ok(usuarios);
    }

    [HttpGet("usuarios/{id}")]
    [Authorize(Roles = "root")]
    [ProducesResponseType(typeof(Usuario), 200)]
    public IActionResult GetUsuarioById(int id)
    {
        var usuario = _usuariosService.GetUsuarioById(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return Ok(usuario);
    }
    [HttpPost("usuarios/Add")]
    public IActionResult CreateUsuario([FromBody] Usuario nuevo)
    {
        if (nuevo == null)
        {
            return BadRequest("El usuario no puede ser nulo.");
        }

        var creado = _usuariosService.CreateUsuario(nuevo);
        if (creado == null)
        {
            return BadRequest("Error al crear el usuario.");
        }

        return CreatedAtAction(nameof(GetUsuarioById), new { id = creado.id_us }, creado);
    }

    // PUT: api/usuarios/5
    [HttpPut("usuarios/update/{id}")]
    public IActionResult UpdateUsuario(int id, [FromBody] Usuario actualizado)
    {
        if (actualizado == null || actualizado.id_us != id)
        {
            return BadRequest("Datos inválidos para actualizar el usuario.");
        }

        var usuarioExistente = _usuariosService.GetUsuarioById(id);
        if (usuarioExistente == null)
        {
            return NotFound();
        }

        var usuarioActualizado = _usuariosService.UpdateUsuario(actualizado);
        if (usuarioActualizado == null)
        {
            return BadRequest("Error al actualizar el usuario.");
        }

        return Ok(usuarioActualizado);
    }

    // DELETE: api/usuarios/5
    [HttpDelete("usuarios/delete/{id}")]
    public IActionResult DeleteUsuario(int id)
    {
        bool eliminado = _usuariosService.DeleteUsuario(id);
        if (!eliminado)
            return NotFound("Usuario no encontrado.");
        return NoContent(); // Devuelve 204 No Content si la eliminación es exitosa
    }
}