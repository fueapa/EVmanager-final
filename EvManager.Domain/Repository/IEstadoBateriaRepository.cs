using EvManager.Domain.Entities;

namespace EvManager.Domain.RepositoryInterfaces;

public interface IEstadoBateriaRepository
{
    Task<IEnumerable<EstadoBateria>> GetAllAsync();
    Task<EstadoBateria> GetByIdAsync(int id);
    Task AddAsync(EstadoBateria estado);
    Task UpdateAsync(EstadoBateria estado);
    Task DeleteAsync(int id);
}