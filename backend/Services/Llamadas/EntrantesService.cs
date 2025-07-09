using backend.Db;
using backend.Models.Llamadas.Entrantes;
using backend.Interfaces.Llamadas;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace backend.Services.Llamadas.Entrantes;

public class EntrantesService : IEntrantesServices
{
    private readonly AppDbContext _context;

    public EntrantesService(AppDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable <LlamadasEnt> GetEntrantes()
    {
        return _context.LlamadasEntrantes.ToList();
    }

    public async Task<LlamadasEnt> GetByIdAsync(int id)
    {
        return await _context.LlamadasEntrantes.FindAsync(id);
    }
    public async Task<LlamadasEnt> CreateAsync(LlamadasEnt llamada)
    {
        _context.LlamadasEntrantes.Add(llamada);
        await _context.SaveChangesAsync();
        return llamada;
    }

    public async Task<LlamadasEnt> UpdateAsync(int id, LlamadasEnt llamada)
    {
        var existingLlamada = await _context.LlamadasEntrantes.FindAsync(id);
        if (existingLlamada == null)
        {
            return null; // Or throw an exception
        }

        existingLlamada.fechaEn = llamada.fechaEn;
        existingLlamada.nombre_areaEn = llamada.nombre_areaEn;
        existingLlamada.responsableEn = llamada.responsableEn;
        existingLlamada.hora_llamadaEn = llamada.hora_llamadaEn;
        existingLlamada.llamEn = llamada.llamEn;
        existingLlamada.seguimientoEn = llamada.seguimientoEn;
        existingLlamada.asunto_llamadaEn = llamada.asunto_llamadaEn;
        existingLlamada.Usuario = llamada.Usuario;

        await _context.SaveChangesAsync();
        return existingLlamada;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var llamada = await _context.LlamadasEntrantes.FindAsync(id);
        if (llamada == null)
        {
            return false; // Or throw an exception
        }

        _context.LlamadasEntrantes.Remove(llamada);
        await _context.SaveChangesAsync();
        return true;
    }

    // Implement other methods as needed, e.g., GetByIdAsync, CreateAsync, UpdateAsync, DeleteAsync
}