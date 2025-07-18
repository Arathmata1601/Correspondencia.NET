using backend.Interfaces.Complementos;
using backend.Models.Documents.Introduccion;
using backend.Models.Documents.Despedida;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers.Documents.Complementos
{
    [ApiController]
    [Route("api/complementos")] // Ruta más limpia y consistente
    [Authorize] // Solo usuarios autenticados
    public class ComplementosController : ControllerBase
    {
        private readonly IComplementosService _complementosService;

        public ComplementosController(IComplementosService complementosService)
        {
            _complementosService = complementosService;

        }

        [HttpGet("despedidas")]
        public async Task<ActionResult<List<Despedida>>> GetDespedidas()
        {

            try
            {
                var despedidas = await _complementosService.GetDespedida();
                return Ok(despedidas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        [HttpGet("introducciones")]
        public async Task<ActionResult<List<Introduccion>>> GetIntroduccion()
        {
           
            try
            {
                var introduccion = await _complementosService.GetIntroduccion();
                return Ok(introduccion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }


}