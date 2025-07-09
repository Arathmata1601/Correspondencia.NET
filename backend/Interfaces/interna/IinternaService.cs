using backend.Models.Recibidos.Internos;

namespace backend.Interfaces.Recibidos.Internos;
public interface IInternaService
{
    Task<List<InternaModel>> GetAllAsync();
    Task<InternaModel> GetByIdAsync(int id);
    Task AddAsync(InternaModel interna);
    Task UpdateAsync(InternaModel interna);
    Task DeleteAsync(int id);
}