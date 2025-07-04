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
        [HttpGet]
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

        //GET api/documents/{id}
        [HttpGet("getById/{id}")]
        
        public  IActionResult GetDocumentById(int id)
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
        [HttpPut("{id}")]
        public  IActionResult UpdateDocument(int id, [FromBody] Documento document)
        {
            if (document == null)
            {
                return BadRequest("Los datos del documento no pueden estar vacíos");
            }

            if (id != document.id_doc)
            {
                return BadRequest("El ID del documento no coincide");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedDocument =  _documentService.UpdateDocument(id, document);
                if (updatedDocument == null)
                {
                    return NotFound($"Documento con ID {id} no encontrado");
                }
                return Ok(updatedDocument);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar documento: {ex.Message}");
            }
        }

        // DELETE api/documents/{id}
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