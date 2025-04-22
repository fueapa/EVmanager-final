using EvManager.Domain.Entities;

namespace EvManager.Domain.RepositoryInterfaces;

public interface IVehiculoRepository
{
    Task<IEnumerable<Vehiculo>> GetAllAsync();
    Task<Vehiculo> GetByIdAsync(int id);
    Task AddAsync(Vehiculo vehiculo);
    Task UpdateAsync(Vehiculo vehiculo);
    Task DeleteAsync(int id);
}