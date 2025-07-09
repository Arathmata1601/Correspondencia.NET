using backend.Db;
using backend.Models.Recibidos.Externos;
using backend.Interfaces.Recibidos.Externos;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Recibidos.Externos
{

    public class ExternaService : IExternaService
    {
        private readonly AppDbContext _context;

        public ExternaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Externa>> GetAllAsync()
        {
            return await _context.Externa.ToListAsync();
        }

        public async Task<Externa> GetByIdAsync(int id)
        {
            return await _context.Externa.FindAsync(id);
        }

        public async Task AddAsync(Externa externa)
        {
            _context.Externa.Add(externa);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Externa externa)
        {
            _context.Externa.Update(externa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var externa = await GetByIdAsync(id);
            if (externa != null)
            {
                _context.Externa.Remove(externa);
                await _context.SaveChangesAsync();
            }
        }
    }
}