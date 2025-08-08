using backend.Interfaces.Documents;
using backend.Models.Documents;
using backend.Db;
using Microsoft.EntityFrameworkCore;
using backend.Models.Documents;
using backend.Models.Documents.otros;
using backend.Models.Areas;

namespace backend.Services.Documents
{
    public class DocumentService : IDocumentService
    {
        private readonly AppDbContext _context;

        public DocumentService(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los documentos con sus CCP
        public async Task<List<Documento>> GetDocuments()
        {
            return await _context.Documento
                .Include(d => d.ConCopias)
                //.Include(d => d.nombre_otro)
                .ToListAsync();
        }

        // Obtener documentos por área con sus CCP
        public async Task<List<Documento>> GetDocumentsByAreaAsync(string area_pro)
        {
            return await _context.Documento
                .Where(d => d.area_pro == area_pro)
                .Include(d => d.ConCopias)
                //.Include(d => d.nombre_otro)
                .ToListAsync();
        }

        // Obtener un documento por ID (sin async)
        public Documento? GetDocumentById(int id)
        {
            return _context.Documento
                .Include(d => d.ConCopias)
                //.Include(d => d.nombre_otro)
                .FirstOrDefault(d => d.id_doc == id);
                
        }


        // Obtener un documento por ID (async) - usado para generar PDF
        public async Task<Documento?> GetDocumentByIdAsync(int id)
        {
            return await _context.Documento
                .Include(d => d.ConCopias)
                //.Include(d => d.nombre_otro)
                .FirstOrDefaultAsync(d => d.id_doc == id);
        }
        //Obtener área por nombre
          public async Task<Area> GetAreaByNombreAsync(string nombre_area)
        {
            return await _context.Areas.FirstOrDefaultAsync(a => a.nombre_area == nombre_area);
        }
        //Obtener área por responsable
          public async Task<Area> GetAreaByResponsableAsync(string responsable)
        {
            return await _context.Areas.FirstOrDefaultAsync(a => a.responsable == responsable);
        }

        // Crear documento con CCP
        public async Task<Documento> CreateDocumentWithCcpAsync(DocumentoCreateDto dto, string usuarioEmisor)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Buscar área del usuario emisor
                var areaPro = await _context.Usuarios
                    .Where(u => u.usuario == usuarioEmisor)
                    .Select(u => u.area)
                    .FirstOrDefaultAsync();

                // Buscar responsable del área receptora
                var usuarioReceptor = await _context.Areas
                    .Where(a => a.nombre_area == dto.area_rec)
                    .Select(a => a.responsable)
                    .FirstOrDefaultAsync();

                // Responsable de atención si aplica
                string? responsableAtencion = null;
                if (!string.IsNullOrEmpty(dto.atencion))
                {
                    responsableAtencion = await _context.Areas
                        .Where(a => a.nombre_area == dto.atencion)
                        .Select(a => a.responsable)
                        .FirstOrDefaultAsync();
                }

                // Crear documento principal
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
                    lugarDOc = "Zacatecas, Zac."
                };

                _context.Documento.Add(documento);
                await _context.SaveChangesAsync();

                // CCP internos
                foreach (var responsableId in dto.conCopiaResponsablesIds)
                {
                    var ccp = new CcpModel
                    {
                        id_docCC = documento.id_doc,
                        responsableDoc = responsableId
                    };
                    _context.ConCopias.Add(ccp);
                }

                // CCP externo (Otros)
                if (!string.IsNullOrEmpty(dto.nombre_otro) && !string.IsNullOrEmpty(dto.puesto_otro))
                {
                    var otro = new OtrosCcpModel
                    {
                        id_doc = documento.id_doc,
                        nombre_otro = dto.nombre_otro,
                        puesto_otro = dto.puesto_otro
                    };
                    _context.OtrosCcp.Add(otro);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return documento;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // Actualizar documento
        public async Task<Documento> UpdateDocument(int id, DocumentoUpdateDto document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document), "El documento no puede ser nulo");

            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));

            var existingDocument = await _context.Documento.FirstOrDefaultAsync(d => d.id_doc == id);
            if (existingDocument == null)
                throw new KeyNotFoundException($"Documento con ID {id} no encontrado");

            if (document.noMemo.HasValue && document.noMemo.Value != 0)
                existingDocument.noMemo = document.noMemo.Value;

            if (document.fechaDoc.HasValue)
                existingDocument.fechaDoc = document.fechaDoc.Value;

            if (!string.IsNullOrWhiteSpace(document.asuntoDoc))
                existingDocument.asuntoDoc = document.asuntoDoc;

            if (!string.IsNullOrWhiteSpace(document.descripcion))
                existingDocument.descripcion = document.descripcion;

            if (!string.IsNullOrWhiteSpace(document.area_rec))
                existingDocument.area_rec = document.area_rec;

            if (!string.IsNullOrWhiteSpace(document.despedida))
                existingDocument.despedida = document.despedida;

            if (!string.IsNullOrWhiteSpace(document.introduccion))
                existingDocument.introduccion = document.introduccion;

            if (!string.IsNullOrWhiteSpace(document.elaborado))
                existingDocument.elaborado = document.elaborado;

            if (!string.IsNullOrWhiteSpace(document.atencion))
                existingDocument.atencion = document.atencion;

            existingDocument.lugarDOc = "Zacatecas, Zac.";

            await _context.SaveChangesAsync();
            return existingDocument;
        }

        // Eliminar documento
        public async Task<bool> DeleteDocument(int id)
        {
            var document = await _context.Documento.FindAsync(id);
            if (document == null)
                return false;

            _context.Documento.Remove(document);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
