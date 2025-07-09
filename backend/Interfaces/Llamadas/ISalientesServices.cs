
using backend.Models.Llamadas.Salientes;

namespace backend.Interfaces.Llamadas;
public interface ISalientesServices
{
    IEnumerable<LlamadasSal> GetSalientes();
    Task<LlamadasSal> GetByIdAsync(int id);
    Task<LlamadasSal> CreateAsync(LlamadasSal llamada);
    Task<LlamadasSal> UpdateAsync(int id, LlamadasSal llamada);
    Task<bool> DeleteAsync(int id);
}