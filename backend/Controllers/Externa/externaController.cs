using backend.Db;
using backend.Models.Recibidos.Externos;
using backend.Interfaces.Recibidos.Externos;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace backend.Services.Recibidos
{
    [ApiController]
    [Route("api/")]
    public class ExternaController : ControllerBase
    {
        private readonly IExternaService _externaService;

        public ExternaController(IExternaService externaService)
        {
            _externaService = externaService;
        }

        [HttpGet("externa")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _externaService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("externa/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _externaService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("externa")]
        public async Task<IActionResult> Create([FromBody] Externa externa)
        {
            if (externa == null) return BadRequest();
            await _externaService.AddAsync(externa);
            return CreatedAtAction(nameof(GetById), new { id = externa.id_ext }, externa);
        }

        [HttpPut("externa/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Externa externa)
        {
            if (id != externa.id_ext || externa == null) return BadRequest();
            await _externaService.UpdateAsync(externa);
            return NoContent();
        }

        [HttpDelete("externa/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _externaService.DeleteAsync(id);
            return NoContent();
        }
    }
    
}