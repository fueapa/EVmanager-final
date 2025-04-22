using EvManager.Domain.Entities;
using EvManager.Domain.RepositoryInterfaces;
using EvManager.Domain.Exceptions;
namespace EvManager.Application.Services;

public class EstadoBateriaService
{
    private readonly IEstadoBateriaRepository _repository;

    public EstadoBateriaService(IEstadoBateriaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EstadoBateria>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<EstadoBateria> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(EstadoBateria estado)
    {
        if (estado.NivelCarga < 0 || estado.NivelCarga > 100)
            throw new EstadoBateriaException("El nivel de carga debe estar entre 0 y 100.");
        await _repository.AddAsync(estado);
    }

    public async Task UpdateAsync(EstadoBateria estado)
    {
        await _repository.UpdateAsync(estado);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}