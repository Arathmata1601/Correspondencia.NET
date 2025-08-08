using backend.Interfaces.Documents;
using backend.Models.Documents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Text;


namespace backend.Controllers.Documents
{
    [ApiController]
    [Route("api/documents")] // Ruta más limpia y consistente
    [Authorize] // Solo usuarios autenticados
    public class DocumentoController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IConverter _converter;

        public DocumentoController(IDocumentService documentService, IConverter converter)
        {
            _documentService = documentService;
            _converter = converter;
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
        //ENPOITNT para crear un documento PDF y mostrarlo en el navegador
        [HttpGet("pdf/{id}")]
        [Authorize]
        public async Task<IActionResult> GetDocumentPdf(int id)
        {
            var documento = await _documentService.GetDocumentByIdAsync(id);

            if (documento == null)
                return NotFound("Documento no encontrado");

            // Convertir imágenes para header y footer
            var headerImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "header.png");
            var footerImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "footer.png");

            // Si no existe header.png, usar footer.png temporalmente
            if (!System.IO.File.Exists(headerImagePath))
                headerImagePath = footerImagePath;

            if (!System.IO.File.Exists(footerImagePath))
                return BadRequest("Archivo footer.png no encontrado");

            var headerBase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(headerImagePath));
            var footerBase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(footerImagePath));

            string rAten;
            string atencionFinal;

            if (documento.atencion == null || documento.atencion.Trim() == "")
            {
                atencionFinal = "";
                rAten = "";
            }
            else
            {
                var responsableAtencion = await _documentService.GetAreaByResponsableAsync(documento.atencion);
                atencionFinal = documento.atencion;
                rAten = responsableAtencion?.nombre_area ?? "";
            }


            //obtener el responsable del area de procedencia
            var area = await _documentService.GetAreaByNombreAsync(documento.area_pro);
            var responsable = area?.responsable;
            var puesto_responsable = area?.Puesto;



            // HTML principal con header y footer integrados
            var html = $@"
            <html>
                <head>
                    <meta charset='utf-8'>
                    <style>
                        body {{ 
                            font-family: Montserrat, sans-serif; 
                            margin: 0;
                            padding: 0;
                            font-size: 14px;
                            height: 100vh;
                        }}
                        
                        .page {{
                            height: 100vh;
                            display: flex;
                            flex-direction: column;
                            position: relative;
                            padding: 20px;
                            box-sizing: border-box;
                        }}
                        
                        .header {{
                            width: 100%;
                            text-align: center;
                            margin-bottom: 20px;
                        }}
                        
                        .header img {{
                            width: 100%;
                            max-height: 220px;
                            height: auto;
                        }}
                        
                        .content {{
                            flex: 1 0 auto;
                            padding: 0 40px;
                        }}
                        
                        .memo-number {{ 
                            text-align: right; 
                            font-size: 18px; 
                            margin-bottom: 5px;
                            font-weight: normal;
                        }}
                        
                        .recipient-info {{ 
                            margin: 5px 0;
                            font-size: 18px;
                            font-weight: bold;
                            width: 300px;
                        }}
                        .atencion {{
                            font-size: 18px;
                            margin-top: 10px;
                            text-align: end;
                            font-weight: bold;
                        }}
                        
                        .subject-line {{ 
                            margin: 5px 0;
                            font-size: 14px;
                        }}
                        
                        .content-section {{ 
                            margin: 80px 0;
                            line-height: 1.4;
                            text-align: justify;
                            font-size: 18px;
                        }}
                        
                        .signature {{ 
                            margin-top: 40px; 
                            text-align: center;
                            font-size: 22px;
                            font-weight: bold;
                        }}
                        
                        .atentamente {{
                            text-align: center;
                            margin: 30px 0 20px 0;
                            font-weight: bold;
                            font-size: 22px;
                            
                        }}
                        
                        .footer {{
                            width: 100%;
                            text-align: center;
                            flex-shrink: 0;
                            padding-bottom: 20px;
                        }}
                        
                        .footer img {{
                            width: 100%;
                            max-height: 80px;
                            height: auto;
                        }}
                        .espacio {{
                            margin-top: 1px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='page'>
                        <!-- Header -->
                        <div class='header'>
                            <img src='data:image/png;base64,{headerBase64}' alt='Header' />
                        </div>
                        
                        <!-- Contenido -->
                        <div class='content'>
                            <div class='memo-number'>
                                Memorándum S.T. No. {documento.noMemo}/{documento.fechaDoc.Year}<br/>
                                Zacatecas, Zac. {documento.fechaDoc:dd} de {GetMonthName(documento.fechaDoc.Month)} de {documento.fechaDoc.Year}
                            </div>

                            <div class='recipient-info'>
                                {documento.area_rec.ToUpper()}
                                <p class='espacio'>{documento.usuarioReceptor.ToUpper()}</p>
                                
                            </div>
                            <div class='atencion'>
                                At´n {documento.atencion?.ToUpper()}
                                <p class='espacio'>{rAten.ToUpper()}</p>
                            </div>

                            <div class='content-section'>
                                {CleanHtmlContent($"{documento.introduccion}{documento.descripcion}")}
                                <p>{documento.despedida}</p>
                            </div>

                            <div class='atentamente'>
                                ATENTAMENTE
                            </div>

                            <div class='signature'>
                                {responsable.ToUpper()}
                                <p class='espacio'>{puesto_responsable.ToUpper()}</p>
                            </div>
                        </div>
                        
                        <!-- Footer -->
                        <div class='footer'>
                            <img src='data:image/png;base64,{footerBase64}' alt='Footer' />
                        </div>
                    </div>
                </body>
            </html>";

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                    Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 }, // Márgenes mínimos
                    DocumentTitle = $"Memo_{documento.noMemo}.pdf"
                },
                Objects = {
            new ObjectSettings
            {
                HtmlContent = html,
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { Line = false },
                FooterSettings = { Line = false }
            }
        }
            };

            var pdfBytes = _converter.Convert(doc);

            return File(pdfBytes, "application/pdf", $"Memo_{documento.noMemo}.pdf");
        }
        // Método auxiliar para limpiar contenido HTML y saltos de línea
        private string CleanHtmlContent(string content)
        {
            if (string.IsNullOrEmpty(content))
                return string.Empty;

            // Remover etiquetas HTML básicas pero conservar el texto
            content = content.Replace("<br>", " ")
                            .Replace("<br/>", " ")
                            .Replace("<br />", " ")
                            .Replace("<p>", "")
                            .Replace("</p>", " ")
                            .Replace("<div>", "")
                            .Replace("</div>", " ")
                            .Replace("\n", " ")
                            .Replace("\r", " ");

            // Limpiar múltiples espacios en blanco
            while (content.Contains("  "))
            {
                content = content.Replace("  ", " ");
            }

            return content.Trim();
        }

        // Método auxiliar para obtener el nombre del mes en español
        private string GetMonthName(int month)
        {
            return month switch
            {
                1 => "enero",
                2 => "febrero",
                3 => "marzo",
                4 => "abril",
                5 => "mayo",
                6 => "junio",
                7 => "julio",
                8 => "agosto",
                9 => "septiembre",
                10 => "octubre",
                11 => "noviembre",
                12 => "diciembre",
                _ => "enero"
            };
        }


        // POST api/documents
        [Authorize]

        [HttpPost("addDocument")]

        public async Task<IActionResult> CreateDocumentWithCcpAsync([FromBody] DocumentoCreateDto document)
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