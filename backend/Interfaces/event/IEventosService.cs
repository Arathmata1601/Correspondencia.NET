 using backend.Models.Events;
 namespace backend.Interfaces.Events;

    public interface IEventosService
    {
        IEnumerable <Eventos> GetEventos();
        Task<List<Eventos>> GetEventoByAreaAsync(string area);
        Task<Eventos> GetEventoByIdAsync(int id);
        Task AddEventoAsync(Eventos evento);
        Task UpdateEventoAsync(Eventos evento);
        Task DeleteEventoAsync(int id);
    }
