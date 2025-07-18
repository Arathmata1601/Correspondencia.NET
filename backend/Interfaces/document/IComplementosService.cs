using backend.Models.Documents.Despedida;
using backend.Models.Documents.Introduccion;

namespace backend.Interfaces.Complementos;
public interface IComplementosService
{
    Task<List<Introduccion>> GetIntroduccion();
    Task<List<Despedida>> GetDespedida();
}