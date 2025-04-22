using EvManager.Domain.Entities;

namespace EvManager.Domain.RepositoryInterfaces;

public interface IEstacionCargaRepository
{
    Task<IEnumerable<EstacionCarga>> GetAllAsync();
    Task<EstacionCarga> GetByIdAsync(int id);
    Task AddAsync(EstacionCarga estacion);
    Task UpdateAsync(EstacionCarga estacion);
    Task DeleteAsync(int id);
}