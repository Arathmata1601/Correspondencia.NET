using backend.Db;
using backend.Models.Recibidos.Internos;
using backend.Interfaces.Recibidos.Internos;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Recibidos.Internos
{
    public class InternaService : IInternaService
    {
        private readonly AppDbContext _context;

        public InternaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<InternaModel>> GetAllAsync()
        {
            return await _context.Interna.ToListAsync();
        }

        public async Task<InternaModel> GetByIdAsync(int id)
        {
            return await _context.Interna.FindAsync(id);
        }

        public async Task AddAsync(InternaModel interna)
        {
            _context.Interna.Add(interna);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(InternaModel interna)
        {
            _context.Interna.Update(interna);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var interna = await GetByIdAsync(id);
            if (interna != null)
            {
                _context.Interna.Remove(interna);
                await _context.SaveChangesAsync();
            }
        }
    }
}