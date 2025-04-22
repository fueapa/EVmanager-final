using EvManager.Domain.Entities;
using EvManager.Domain.RepositoryInterfaces;
using EvManager.Domain.Exceptions; // Añadido para PlanRutaException

namespace EvManager.Application.Services;

public class PlanRutaService
{
    private readonly IPlanRutaRepository _repository;

    public PlanRutaService(IPlanRutaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PlanRuta>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<PlanRuta> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(PlanRuta plan)
    {
        if (string.IsNullOrEmpty(plan.UbicacionInicio) || string.IsNullOrEmpty(plan.UbicacionFin))
            throw new PlanRutaException("Las ubicaciones de inicio y fin no pueden estar vacías.");
        await _repository.AddAsync(plan);
    }

    public async Task UpdateAsync(PlanRuta plan)
    {
        await _repository.UpdateAsync(plan);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}