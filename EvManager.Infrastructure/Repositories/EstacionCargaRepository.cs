using Microsoft.EntityFrameworkCore;
using EvManager.Domain.Entities;
using EvManager.Domain.RepositoryInterfaces;
using EvManager.Infrastructure.Contexts;
using EvManager.Domain.Exceptions;
namespace EvManager.Infrastructure.Repositories;

public class EstacionCargaRepository : IEstacionCargaRepository
{
    private readonly EvManagerContext _context;

    public EstacionCargaRepository(EvManagerContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EstacionCarga>> GetAllAsync()
    {
        return await _context.EstacionesCarga.Where(ec => ec.IsActive).ToListAsync();
    }

    public async Task<EstacionCarga> GetByIdAsync(int id)
    {
        var estacion = await _context.EstacionesCarga.FindAsync(id);
        if (estacion == null || !estacion.IsActive)
            throw new EstacionCargaException($"Estación de carga con ID {id} no encontrada.");
        return estacion;
    }

    public async Task AddAsync(EstacionCarga estacion)
    {
        await _context.EstacionesCarga.AddAsync(estacion);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EstacionCarga estacion)
    {
        _context.EstacionesCarga.Update(estacion);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var estacion = await GetByIdAsync(id);
        estacion.IsActive = false;
        await _context.SaveChangesAsync();
    }
}