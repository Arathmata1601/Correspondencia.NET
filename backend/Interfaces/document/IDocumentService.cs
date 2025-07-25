using backend.Models.Documents;
 

namespace backend.Interfaces.Documents;

public interface IDocumentService
{
    Task<List<Documento>> GetDocuments();
    Task<List<Documento>> GetDocumentsByAreaAsync(string area_pro);
    Documento GetDocumentById(int id);
    Task<Documento> CreateDocumentWithCcpAsync(DocumentoCreateDto document, string usuarioEmisor);
    Task<Documento> UpdateDocument(int id, DocumentoUpdateDto document);
    //Documento UpdateDocument(int id, Documento document);
    Task<bool> DeleteDocument(int id);
}