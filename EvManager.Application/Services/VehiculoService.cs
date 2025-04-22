using EvManager.Domain.Entities;
using EvManager.Domain.RepositoryInterfaces;

namespace EvManager.Application.Services;

public class VehiculoService
{
    private readonly IVehiculoRepository _repository;

    public VehiculoService(IVehiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Vehiculo>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Vehiculo> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(Vehiculo vehiculo)
    {
        if (string.IsNullOrEmpty(vehiculo.Matricula))
            throw new Exception("La matrícula del vehículo no puede estar vacía.");
        await _repository.AddAsync(vehiculo);
    }

    public async Task UpdateAsync(Vehiculo vehiculo)
    {
        await _repository.UpdateAsync(vehiculo);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}