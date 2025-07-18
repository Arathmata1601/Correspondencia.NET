using backend.Interfaces.Complementos;
using backend.Db;
using backend.Models.Documents.Despedida;
using backend.Models.Documents.Introduccion;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Documents.Complementos;

public class complementosService : IComplementosService
{
    private readonly AppDbContext _context;
    public complementosService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<Introduccion>> GetIntroduccion()
    {
        return await _context.Introduccion.ToListAsync();
    }

    public async Task<List<Despedida>> GetDespedida()
    {
        return await _context.Despedida.ToListAsync();
    }
}

