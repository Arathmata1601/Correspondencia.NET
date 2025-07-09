using backend.Db;
using backend.Models.Llamadas.Entrantes;
using backend.Interfaces.Llamadas;
using backend.Services.Llamadas.Entrantes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers.Llamadas
{
    [ApiController]
    [Route("api/")]

    public class EntrantesController : ControllerBase
    {
        private readonly IEntrantesServices _EntrantesService;

        public EntrantesController(IEntrantesServices EntrantesService)
        {
            _EntrantesService = EntrantesService;
        }

        [HttpGet("entrantes")]
        [Authorize]
        public IActionResult GetEntrantes()
        {
            var entrante = _EntrantesService.GetEntrantes();
            return Ok(entrante);
        }
        [HttpGet("entrantes/{id}", Name = "GetEntranteById")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var entrante = await _EntrantesService.GetByIdAsync(id);
            if (entrante == null)
            {
                return NotFound();
            }
            return Ok(entrante);
        }
        [HttpPost("entrantes")]
        [Authorize]     
        public async Task<IActionResult> CreateAsync([FromBody] LlamadasEnt llamada)
        {
            if (llamada == null)
            {
                return BadRequest("Llamada no puedes ser nula.");
            }

            var usuarioEnt = User.Identity?.Name;
            llamada.Usuario = usuarioEnt;

            var createdLlamada = await _EntrantesService.CreateAsync(llamada);
            return CreatedAtRoute("GetEntranteById", new { id = createdLlamada.id_llamadaEn }, createdLlamada);

        }
        [HttpPut("entrantes/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] LlamadasEnt llamada)
        {
            if (llamada == null)
            {
                return BadRequest("Llamada no puedes ser nula.");
            }

            var updatedLlamada = await _EntrantesService.UpdateAsync(id, llamada);
            if (updatedLlamada == null)
            {
                return NotFound();
            }
            return Ok(updatedLlamada);
        }
        
        [HttpDelete("entrantes/{id}")]
        [Authorize]     
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _EntrantesService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }


    }
    
}