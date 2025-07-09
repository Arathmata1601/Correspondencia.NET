using backend.Db;
using backend.Models.Events;
using backend.Interfaces.Events;
using backend.Services.Events;
using Microsoft.EntityFrameworkCore;        
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.Events
{
    [ApiController]
    [Route("api/")]
   
    public class EventosController : ControllerBase
    {
        private readonly IEventosService _eventosService;

        public EventosController(IEventosService eventosService)
        {
            _eventosService = eventosService;
        }
        [HttpGet("eventos")]
        [Authorize]
        public  IActionResult GetEventos()
        {
            var evento = _eventosService.GetEventos();
            return Ok(evento);
        }
        [HttpGet("eventos/{id}")]
        [Authorize]
        public async Task<IActionResult> GetEventoById(int id)
        {
            var evento = await _eventosService.GetEventoByIdAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            return Ok(evento);
        }
        [HttpPost("eventos")]
        [Authorize]
        public async Task<IActionResult> AddEvento([FromBody] Eventos evento)
        {
            if (evento == null)
            {
                return BadRequest("Evento no puede ser nulo.");
            }
            
            var area = User.FindFirst("area")?.Value;
            evento.area = area;

            await _eventosService.AddEventoAsync(evento);
            return CreatedAtAction(nameof(GetEventoById), new { id = evento.id_evento }, evento);
        }
        [HttpPut("eventos/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateEvento(int id, [FromBody] Eventos evento)
        {
            if (evento == null || evento.id_evento != id)
            {
                return BadRequest("Evento no válido.");
            }

            var existingEvento = await _eventosService.GetEventoByIdAsync(id);
            if (existingEvento == null)
            {
                return NotFound();
            }

            await _eventosService.UpdateEventoAsync(evento);
            return NoContent();
        }
        [HttpDelete("eventos/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            var evento = await _eventosService.GetEventoByIdAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            await _eventosService.DeleteEventoAsync(id);
            return NoContent();
        }
    }
}