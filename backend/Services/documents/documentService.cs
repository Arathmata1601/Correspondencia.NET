using backend.Interfaces.Documents;
using backend.Models.Documents;
using backend.Db;
using Microsoft.EntityFrameworkCore;
using backend.Models.Documents.ccp;
using backend.Models.Documents.otros;
namespace backend.Services.Documents
{
    public class DocumentService : IDocumentService
    {
        private readonly AppDbContext _context;

        public DocumentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Documento>> GetDocuments()
        {
            return await _context.Documento.ToListAsync();
        }
        public Documento? GetDocumentById(int id)
        {
            return _context.Documento.FirstOrDefault(d => d.id_doc == id);
        }
        public async Task<Documento> CreateDocumentWithCcpAsync(
            DocumentoCreateDto dto, string usuarioEmisor)
        {
            // 🔹 Buscar el área del usuario emisor
            var areaPro = await _context.Usuarios
                .Where(u => u.usuario == usuarioEmisor)
                .Select(u => u.area)
                .FirstOrDefaultAsync();

            // 🔹 Buscar responsable del área receptora
            var usuarioReceptor = await _context.Areas
                .Where(a => a.nombre_area == dto.area_rec)
                .Select(a => a.responsable)
                .FirstOrDefaultAsync();

            // 🔹 Buscar responsable del área de atención si aplica
            string? responsableAtencion = null;
            if (!string.IsNullOrEmpty(dto.atencion))
            {
                responsableAtencion = await _context.Areas
                    .Where(a => a.nombre_area == dto.atencion)
                    .Select(a => a.responsable)
                    .FirstOrDefaultAsync();
            }

            var documento = new Documento
            {
                noMemo = dto.noMemo,
                area_pro = areaPro,
                fechaDoc = dto.fechaDoc,
                asuntoDoc = dto.asuntoDoc,
                descripcion = dto.descripcion,
                usuarioEmisor = usuarioEmisor,
                usuarioReceptor = usuarioReceptor,
                estatusDoc = "Pendiente",
                area_rec = dto.area_rec,
                despedida = dto.despedida,
                introduccion = dto.introduccion,
                elaborado = dto.elaborado,
                atencion = dto.atencion,
                lugarDOc = "Zacatecas, Zac." // constante como en PHP
            };

            // 🔹 Guardar documento
            _context.Documento.Add(documento);
            await _context.SaveChangesAsync();

            // 🔹 Agregar CCP internos
            foreach (var responsableId in dto.conCopiaResponsablesIds)
            {
                var ccp = new CcpModel
                {
                    id_docCC = documento.id_doc,
                    responsableDoc = responsableId
                };

                _context.ConCopia.Add(ccp);
            }

            await _context.SaveChangesAsync();

            // 🔹 Agregar persona externa
            if (!string.IsNullOrEmpty(dto.nombre_otro) && !string.IsNullOrEmpty(dto.puesto_otro))
            {
                var otro = new OtrosCcpModel
                {
                    nombre_otro = dto.nombre_otro,
                    puesto_otro = dto.puesto_otro
                };

                _context.OtrosCcp.Add(otro);
                await _context.SaveChangesAsync();

                _context.ConCopia.Add(new CcpModel
                {
                    id_docCC = documento.id_doc
                });
            }

            await _context.SaveChangesAsync();
            return documento;
        }

        public Documento UpdateDocument(int id, Documento document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document), "El documento no puede ser nulo");
            }

            var existingDocument = _context.Documento.FirstOrDefault(d => d.id_doc == id);
            if (existingDocument == null)
            {
                throw new KeyNotFoundException($"Documento con ID {id} no encontrado");
            }

            existingDocument.noMemo = document.noMemo;
            existingDocument.area_pro = document.area_pro;
            existingDocument.fechaDoc = document.fechaDoc;
            existingDocument.asuntoDoc = document.asuntoDoc;

            _context.SaveChanges();
            return existingDocument;
        }
        public async Task<bool> DeleteDocument(int id)
        {
            var document = await _context.Documento.FindAsync(id);
            if (document == null)
            {
                return false; // Documento no encontrado
            }

            _context.Documento.Remove(document);
            await _context.SaveChangesAsync();
            return true; // Eliminación exitosa


        }

    }
}
