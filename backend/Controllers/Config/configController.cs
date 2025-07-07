using backend.Models.config;
using Microsoft.AspNetCore.Mvc;
using backend.Interfaces.config;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers.Config
{
    [ApiController]
    [Route("api/")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfigService _configService;

        public ConfigController(IConfigService configService)
        {
            _configService = configService;
        }

        [HttpGet("config")]
        [Authorize(Roles = "root")]
        public IActionResult GetConfig()
        {
            var config = _configService.GetConfig();
            return Ok(config);
        }
/*
        [HttpPost("update/{id}")] // Keeping POST as original, but fixing route
        [Authorize(Roles = "root")]
        public IActionResult UpdateConfig(int id, [FromBody] Configuracion actualizado)
        {
            if (actualizado == null)
            {
                return BadRequest("Datos invalidos de configuracion.");
            }

            // Set the ID to ensure consistency
            actualizado.id = id;

            var result = _configService.UpdateConfig(actualizado); // Fixed: was using 'config' instead of 'actualizado'
            if (result)
            {
                return NoContent();
            }
            return StatusCode(500, "Error al editar configuracion.");
        }*/
    }
}