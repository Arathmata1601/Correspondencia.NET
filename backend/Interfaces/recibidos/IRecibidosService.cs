using backend.Models.Recibidos;

namespace backend.Interfaces.Recibidos
{
    public interface IRecibidosService
    {
        Task<List<DocRecibidos>> GetAllAsync();
        Task<DocRecibidos> GetByIdAsync(int id);
        Task AddAsync(DocRecibidos recibidos);
        Task UpdateAsync(DocRecibidos recibidos);
        Task DeleteAsync(int id);
    }
}   