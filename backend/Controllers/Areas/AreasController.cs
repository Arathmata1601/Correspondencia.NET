using backend.Interfaces.Areas;
using backend.Models.Areas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace backend.Controllers.Areas
{
    [ApiController]
    [Route("api/")]
    public class AreasController : ControllerBase
    {
        private readonly IAreasService _areasService;

        public AreasController(IAreasService areasService)
        {
            _areasService = areasService;
        }

        [HttpGet("areas")]
        [Authorize(Roles = "root")]
        [ProducesResponseType(typeof(IEnumerable<Area>), 200)]
        public IActionResult GetAllAreas()
        {
            var areas = _areasService.GetAllAreas();
            return Ok(areas);
        }

        [HttpGet("areas/{id}")]
        [Authorize(Roles = "root")]
        [ProducesResponseType(typeof(Area), 200)]
        public IActionResult GetAreaById(int id)
        {
            var area = _areasService.GetAreaById(id);
            if (area == null)
            {
                return NotFound();
            }
            return Ok(area);
        }
        [HttpPost("areas/addArea")]
        public IActionResult CreateArea([FromBody] Area nueva)
        {
            if (nueva == null)
            {
                return BadRequest("El área no puede ser nula.");
            }

            var creada = _areasService.CreateArea(nueva);
            if (creada == null)
            {
                return BadRequest("Error al crear el área.");
            }

            return CreatedAtAction(nameof(GetAreaById), new { id = creada.id_area }, creada);
        }
        [HttpPut("areas/updateArea/{id}")]
        public IActionResult UpdateArea(int id, [FromBody] Area actualizada)
        {
            if (actualizada == null || id != actualizada.id_area)
            {
                return BadRequest("Datos inválidos para actualizar el área.");
            }

            var areaExistente = _areasService.GetAreaById(id);
            if (areaExistente == null)
            {
                return NotFound("Área no encontrada.");
            }

           var AreaActualizada = _areasService.UpdateArea(actualizada);
            if (AreaActualizada == null)
                {
                    return BadRequest("Error al actualizar el usuario.");
                }

            return Ok(AreaActualizada);
        }
        [HttpDelete("areas/deleteArea/{id}")]
        public IActionResult DeleteArea(int id)
        {
            var eliminado = _areasService.DeleteArea(id);
            if (!eliminado)
            {
                return NotFound("Área no encontrada.");
            }
            return NoContent(); // Devuelve 204 No Content si la eliminación es exitosa
        }

    }
}