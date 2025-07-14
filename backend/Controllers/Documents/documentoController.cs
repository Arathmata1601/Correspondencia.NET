using backend.Interfaces.Documents;
using backend.Models.Documents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers.Documents
{
    [ApiController]
    [Route("api/documents")] // Ruta más limpia y consistente
    [Authorize] // Solo usuarios autenticados
    public class DocumentoController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentoController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        // GET api/documents
        [HttpGet("todos")]
        public async Task<ActionResult<List<Documento>>> GetDocuments()
        {
           
            try
            {
                var documents = await _documentService.GetDocuments();
                return Ok(documents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetDocumentsByArea()
        {
            var area_pro = User.FindFirst("area")?.Value;

            if (string.IsNullOrEmpty(area_pro))
            {
                return Unauthorized("No se encontró el área en el token.");
            }

            var documents = await _documentService.GetDocumentsByAreaAsync(area_pro);

            if (documents == null || !documents.Any())
            {
                return NotFound();
            }

            return Ok(documents);
        }
 

        //GET api/documents/{id}
        [HttpGet("getById/{id}")]
        [Authorize]
        public IActionResult GetDocumentById(int id)
        {
            try
            {
                var documents = _documentService.GetDocumentById(id);
                if (documents == null)
                {
                    return NotFound($"Documento con ID {id} no encontrado");
                }
                return Ok(documents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST api/documents
        [Authorize]
        [HttpPost()]
        [HttpPost("addDocument")]

        public async Task<IActionResult> CreateDocument([FromBody] DocumentoCreateDto document)
        {
            // Validación básica
            if (document == null)
            {
                return BadRequest("Los datos del documento no pueden estar vacíos");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Obtener el usuario desde el JWT
                var usuarioEmisor = User.Identity?.Name;

                if (string.IsNullOrEmpty(usuarioEmisor))
                {
                    return Unauthorized("No se pudo identificar al usuario emisor.");
                }

                var createdDocument = await _documentService.CreateDocumentWithCcpAsync(document, usuarioEmisor);

                return CreatedAtAction(nameof(GetDocumentById), new { id = createdDocument.id_doc }, createdDocument);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear documento: {ex.Message}");
            }
        }

        // PUT api/documents/{id}
        // Controller actualizado
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, [FromBody] DocumentoUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Los datos del documento no pueden estar vacíos");
            }

            if (id <= 0)
            {
                return BadRequest("El ID debe ser mayor a cero");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedDocument = await _documentService.UpdateDocument(id, dto);
                return Ok(updatedDocument);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar documento: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            try
            {
                var result = await _documentService.DeleteDocument(id);
                if (!result)
                {
                    return NotFound($"Documento con ID {id} no encontrado");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar documento: {ex.Message}");
            }
        }
    }
}