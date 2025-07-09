using backend.Db;
using backend.Models.Recibidos;
using backend.Interfaces.Recibidos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Services.Recibidos
{
    public class RecibidosService : IRecibidosService
    {
        private readonly AppDbContext _context;

        public RecibidosService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DocRecibidos>> GetAllAsync()
        {
            return await _context.Recibidos.ToListAsync();
        }

        public async Task<DocRecibidos> GetByIdAsync(int id)
        {
            return await _context.Recibidos.FindAsync(id);
        }

        public async Task AddAsync(DocRecibidos recibidos)
        {
            _context.Recibidos.Add(recibidos);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DocRecibidos recibidos)
        {
            _context.Recibidos.Update(recibidos);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var recibidos = await GetByIdAsync(id);
            if (recibidos != null)
            {
                _context.Recibidos.Remove(recibidos);
                await _context.SaveChangesAsync();
            }
        }
    }
}