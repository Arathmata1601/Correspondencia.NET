
using backend.Models.Llamadas.Entrantes;

namespace backend.Interfaces.Llamadas;
public interface IEntrantesServices
{
    IEnumerable<LlamadasEnt> GetEntrantes();
    Task<LlamadasEnt> GetByIdAsync(int id);
    Task<LlamadasEnt> CreateAsync(LlamadasEnt llamada);
    Task<LlamadasEnt> UpdateAsync(int id, LlamadasEnt llamada);
    Task<bool> DeleteAsync(int id);
}