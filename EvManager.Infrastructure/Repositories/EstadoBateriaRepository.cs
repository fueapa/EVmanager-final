using Microsoft.EntityFrameworkCore;
using EvManager.Domain.Entities;
using EvManager.Domain.RepositoryInterfaces;
using EvManager.Infrastructure.Contexts;
using EvManager.Domain.Exceptions;
namespace EvManager.Infrastructure.Repositories;

public class EstadoBateriaRepository : IEstadoBateriaRepository
{
    private readonly EvManagerContext _context;

    public EstadoBateriaRepository(EvManagerContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EstadoBateria>> GetAllAsync()
    {
        return await _context.EstadosBateria.Where(eb => eb.IsActive).ToListAsync();
    }

    public async Task<EstadoBateria> GetByIdAsync(int id)
    {
        var estado = await _context.EstadosBateria.FindAsync(id);
        if (estado == null || !estado.IsActive)
            throw new EstadoBateriaException($"Estado de batería con ID {id} no encontrado.");
        return estado;
    }

    public async Task AddAsync(EstadoBateria estado)
    {
        await _context.EstadosBateria.AddAsync(estado);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EstadoBateria estado)
    {
        _context.EstadosBateria.Update(estado);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var estado = await GetByIdAsync(id);
        estado.IsActive = false;
        await _context.SaveChangesAsync();
    }
}