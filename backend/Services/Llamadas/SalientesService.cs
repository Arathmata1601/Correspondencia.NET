using backend.Db;
using backend.Models.Llamadas.Salientes;
using backend.Interfaces.Llamadas;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace backend.Services.Llamadas.Salientes;

public class SalientesService : ISalientesServices
{
    private readonly AppDbContext _context;

    public SalientesService(AppDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable <LlamadasSal> GetSalientes()
    {
        return _context.LlamadasSalientes.ToList();
    }

    public async Task<LlamadasSal> GetByIdAsync(int id)
    {
        return await _context.LlamadasSalientes.FindAsync(id);
    }
    public async Task<LlamadasSal> CreateAsync(LlamadasSal llamada)
    {
        _context.LlamadasSalientes.Add(llamada);
        await _context.SaveChangesAsync();
        return llamada;
    }

    public async Task<LlamadasSal> UpdateAsync(int id, LlamadasSal llamada)
    {
        var existingLlamada = await _context.LlamadasSalientes.FindAsync(id);
        if (existingLlamada == null)
        {
            return null; // Or throw an exception
        }

        existingLlamada.fecha_llamada = llamada.fecha_llamada;
        existingLlamada.area_ = llamada.area_;
        existingLlamada.remitente = llamada.remitente;
        existingLlamada.hora_llamada = llamada.hora_llamada;
        existingLlamada.llam = llamada.llam;
        existingLlamada.seguimiento = llamada.seguimiento;
        existingLlamada.asunto_llamada = llamada.asunto_llamada;
        existingLlamada.usuarioSal = llamada.usuarioSal;

        await _context.SaveChangesAsync();
        return existingLlamada;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var llamada = await _context.LlamadasSalientes.FindAsync(id);
        if (llamada == null)
        {
            return false; // Or throw an exception
        }

        _context.LlamadasSalientes.Remove(llamada);
        await _context.SaveChangesAsync();
        return true;
    }

    // Implement other methods as needed, e.g., GetByIdAsync, CreateAsync, UpdateAsync, DeleteAsync
}