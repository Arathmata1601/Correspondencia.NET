using backend.Db;
using backend.Models.Recibidos;
using backend.Interfaces.Recibidos;
using backend.Services.Recibidos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;  

namespace backend.Controllers.Recibidos
{
    [Route("api/")]
    [ApiController]
    
    public class RecibidosController : ControllerBase
    {
        private readonly IRecibidosService _recibidosService;

        public RecibidosController(IRecibidosService recibidosService)
        {
            _recibidosService = recibidosService;
        }

        [HttpGet("recibidos")]
        public async Task<ActionResult<IEnumerable<DocRecibidos>>> GetAll()
        {
            var recibidos = await _recibidosService.GetAllAsync();
            return Ok(recibidos);
        }

        [HttpGet("recibidos/{id}")]
        public async Task<ActionResult<DocRecibidos>> GetById(int id)
        {
            var recibidos = await _recibidosService.GetByIdAsync(id);
            if (recibidos == null)
            {
                return NotFound();
            }
            return Ok(recibidos);
        }

        [HttpPost("recibidos")]
        public async Task<ActionResult> Create([FromBody] DocRecibidos recibidos)
        {
            if (recibidos == null)
            {
                return BadRequest();
            }
            await _recibidosService.AddAsync(recibidos);
            return CreatedAtAction(nameof(GetById), new { id = recibidos.id_Rec }, recibidos);
        }

        [HttpPut("recibidos/{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] DocRecibidos recibidos)
        {
            if (id != recibidos.id_Rec || recibidos == null)
            {
                return BadRequest();
            }
            await _recibidosService.UpdateAsync(recibidos);
            return NoContent();
        }

        [HttpDelete("recibidos/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _recibidosService.DeleteAsync(id);
            return NoContent();
        }
    }
}
