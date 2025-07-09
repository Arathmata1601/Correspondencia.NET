using backend.Db;
using backend.Models.Recibidos.Internos;
using backend.Interfaces.Recibidos.Internos;
using backend.Services.Recibidos.Internos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers.Internos
{
    [ApiController]
    [Route("api/")]
    public class InternaController : ControllerBase
    {
        private readonly IInternaService _internaService;

        public InternaController(IInternaService internaService)
        {
            _internaService = internaService;
        }

        [HttpGet("interna")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _internaService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("interna/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _internaService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("interna")]
        public async Task<IActionResult> Create([FromBody] InternaModel interna)
        {
            if (interna == null) return BadRequest();
            await _internaService.AddAsync(interna);
            return CreatedAtAction(nameof(GetById), new { id = interna.id_int }, interna);
        }

        [HttpPut("interna/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] InternaModel interna)
        {
            if (id != interna.id_int || interna == null) return BadRequest();
            await _internaService.UpdateAsync(interna);
            return NoContent();
        }

        [HttpDelete("interna/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _internaService.DeleteAsync(id);
            return NoContent();
        }
    }
}