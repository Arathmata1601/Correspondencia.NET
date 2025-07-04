using backend.Models.Documents;
 

namespace backend.Interfaces.Documents;

public interface IDocumentService
{
    Task<List<Documento>> GetDocuments();
    Documento GetDocumentById(int id);
    Task<Documento> CreateDocumentWithCcpAsync(DocumentoCreateDto document, string usuarioEmisor);
    Documento UpdateDocument(int id, Documento document);
    Task<bool> DeleteDocument(int id);
}