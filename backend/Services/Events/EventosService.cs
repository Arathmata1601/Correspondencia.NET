using backend.Db;
using backend.Interfaces.Events;
using backend.Models.Events;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Events
{
    public class EventosService : IEventosService
    {
        private readonly AppDbContext _context;

        public EventosService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Eventos > GetEventos()
        {
            return  _context.Eventos.ToList();
        }

        public async Task<Eventos> GetEventoByIdAsync(int id)
        {
            return await _context.Eventos.FindAsync(id);
        }
        

        public async Task AddEventoAsync(Eventos evento)
        {
            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEventoAsync(Eventos evento)
        {
            var existingEvento = await _context.Eventos.FindAsync(evento.id_evento);
            if (existingEvento != null)
            {
                existingEvento.nombre_evento = evento.nombre_evento;
                existingEvento.descrip = evento.descrip;
                existingEvento.fecha_inicial = evento.fecha_inicial;
                existingEvento.Fecha_final = evento.Fecha_final;
                existingEvento.hora_inicio = evento.hora_inicio;
                existingEvento.hora_fin = evento.hora_fin;
                existingEvento.lugar = evento.lugar;
                existingEvento.area = evento.area;
                //existingEvento.estado = evento.estado; // Uncomment if estado is used

                await _context.SaveChangesAsync();
            }
        }
        
        public async Task DeleteEventoAsync(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
                await _context.SaveChangesAsync();
            }
        }
    }
}