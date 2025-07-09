using backend.Models.Recibidos.Externos;

namespace backend.Interfaces.Recibidos.Externos;
public interface IExternaService
{
    Task<IEnumerable<Externa>> GetAllAsync();
    Task<Externa> GetByIdAsync(int id);
    Task AddAsync(Externa externa);
    Task UpdateAsync(Externa externa);
    Task DeleteAsync(int id);
}