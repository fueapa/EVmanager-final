using EvManager.Domain.Entities;

namespace EvManager.Domain.RepositoryInterfaces;

public interface IPlanRutaRepository
{
    Task<IEnumerable<PlanRuta>> GetAllAsync();
    Task<PlanRuta> GetByIdAsync(int id);
    Task AddAsync(PlanRuta plan);
    Task UpdateAsync(PlanRuta plan);
    Task DeleteAsync(int id);
}