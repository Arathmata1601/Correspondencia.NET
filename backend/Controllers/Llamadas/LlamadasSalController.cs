using backend.Db;
using backend.Models.Llamadas.Salientes;
using backend.Interfaces.Llamadas;
using backend.Services.Llamadas.Salientes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers.Llamadas
{
    [ApiController]
    [Route("api/")]

    public class SalientesController : ControllerBase
    {
        private readonly ISalientesServices _SalientesService;

        public SalientesController(ISalientesServices SalientesService)
        {
            _SalientesService = SalientesService;
        }

        [HttpGet("salientes")]
        [Authorize]
        public IActionResult GetSalientes()
        {
            var saliente = _SalientesService.GetSalientes();
            return Ok(saliente);
        }
        [HttpGet("salientes/{id}", Name = "GetSalientesById")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var saliente = await _SalientesService.GetByIdAsync(id);
            if (saliente == null)
            {
                return NotFound();
            }
            return Ok(saliente);
        }
        [HttpPost("salientes")]
        [Authorize]     
        public async Task<IActionResult> CreateAsync([FromBody] LlamadasSal llamada)
        
        {
            
            if (llamada == null)
            {
                return BadRequest("Llamada no puedes ser nula.");
            }
            var usuarioSal = User.Identity?.Name;
            llamada.usuarioSal = usuarioSal;
            var createdLlamada = await _SalientesService.CreateAsync(llamada);
            return CreatedAtRoute("GetSalientesById", new { id = createdLlamada.id_llamada }, createdLlamada);

        }
        [HttpPut("salientes/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] LlamadasSal llamada)
        {
            if (llamada == null)
            {
                return BadRequest("Llamada no puedes ser nula.");
            }

            var updatedLlamada = await _SalientesService.UpdateAsync(id, llamada);
            if (updatedLlamada == null)
            {
                return NotFound();
            }
            return Ok(updatedLlamada);
        }
        
        [HttpDelete("salientes/{id}")]
        [Authorize]     
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _SalientesService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }


    }
    
}