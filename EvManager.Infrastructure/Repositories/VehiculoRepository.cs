using Microsoft.EntityFrameworkCore;
using EvManager.Domain.Entities;
using EvManager.Domain.Exceptions;
using EvManager.Domain.RepositoryInterfaces;
using EvManager.Infrastructure.Contexts;

namespace EvManager.Infrastructure.Repositories;

public class VehiculoRepository : IVehiculoRepository
{
    private readonly EvManagerContext _context;

    public VehiculoRepository(EvManagerContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Vehiculo>> GetAllAsync()
    {
        return await _context.Vehiculos.Where(v => v.IsActive).ToListAsync();
    }

    public async Task<Vehiculo> GetByIdAsync(int id)
    {
        var vehiculo = await _context.Vehiculos.FindAsync(id);
        if (vehiculo == null || !vehiculo.IsActive)
            throw new Exception($"Vehículo con ID {id} no encontrado.");
        return vehiculo;
    }

    public async Task AddAsync(Vehiculo vehiculo)
    {
        await _context.Vehiculos.AddAsync(vehiculo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Vehiculo vehiculo)
    {
        _context.Vehiculos.Update(vehiculo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var vehiculo = await GetByIdAsync(id);
        vehiculo.IsActive = false;
        await _context.SaveChangesAsync();
    }
}