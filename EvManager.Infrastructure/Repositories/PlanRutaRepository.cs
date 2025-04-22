using Microsoft.EntityFrameworkCore;
using EvManager.Domain.Entities;
using EvManager.Domain.RepositoryInterfaces;
using EvManager.Infrastructure.Contexts;
using EvManager.Domain.Exceptions; // Añadido para PlanRutaException

namespace EvManager.Infrastructure.Repositories;

public class PlanRutaRepository : IPlanRutaRepository
{
    private readonly EvManagerContext _context;

    public PlanRutaRepository(EvManagerContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PlanRuta>> GetAllAsync()
    {
        return await _context.PlanesRuta.Where(pr => pr.IsActive).ToListAsync();
    }

    public async Task<PlanRuta> GetByIdAsync(int id)
    {
        var plan = await _context.PlanesRuta.FindAsync(id);
        if (plan == null || !plan.IsActive)
            throw new PlanRutaException($"Plan de ruta con ID {id} no encontrado.");
        return plan;
    }

    public async Task AddAsync(PlanRuta plan)
    {
        await _context.PlanesRuta.AddAsync(plan);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PlanRuta plan)
    {
        _context.PlanesRuta.Update(plan);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var plan = await GetByIdAsync(id);
        plan.IsActive = false;
        await _context.SaveChangesAsync();
    }
}