using EvManager.Domain.Entities;
using EvManager.Domain.RepositoryInterfaces;
using EvManager.Domain.Exceptions; // Añadido para EstacionCargaException

namespace EvManager.Application.Services;

public class EstacionCargaService
{
    private readonly IEstacionCargaRepository _repository;

    public EstacionCargaService(IEstacionCargaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EstacionCarga>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<EstacionCarga> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(EstacionCarga estacion)
    {
        if (string.IsNullOrEmpty(estacion.Nombre))
            throw new EstacionCargaException("El nombre de la estación de carga no puede estar vacío.");
        await _repository.AddAsync(estacion);
    }

    public async Task UpdateAsync(EstacionCarga estacion)
    {
        await _repository.UpdateAsync(estacion);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}