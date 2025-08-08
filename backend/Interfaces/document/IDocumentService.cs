using backend.Models.Documents;
using backend.Models.Areas;

 

namespace backend.Interfaces.Documents;

public interface IDocumentService
{
    Task<List<Documento>> GetDocuments();
    Task<List<Documento>> GetDocumentsByAreaAsync(string area_pro);
    Documento GetDocumentById(int id);
    Task<Documento?> GetDocumentByIdAsync(int id);
    Task<Area> GetAreaByNombreAsync(string nombre_area);
    Task<Area> GetAreaByResponsableAsync(string responsable);
    Task<Documento> CreateDocumentWithCcpAsync(DocumentoCreateDto document, string usuarioEmisor);
    Task<Documento> UpdateDocument(int id, DocumentoUpdateDto document);
    //Documento UpdateDocument(int id, Documento document);
    Task<bool> DeleteDocument(int id);
}