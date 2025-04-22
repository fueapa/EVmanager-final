using Microsoft.AspNetCore.Mvc;
using EvManager.Application.Services;
using EvManager.Domain.Entities;
using EvManager.Api.Dtos;

namespace EvManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EstadoBateriaController : ControllerBase
{
    private readonly EstadoBateriaService _service;

    public EstadoBateriaController(EstadoBateriaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstadoBateriaDto>>> Get()
    {
        var estados = await _service.GetAllAsync();
        var estadosDto = estados.Select(e => new EstadoBateriaDto
        {
            Id = e.Id,
            VehiculoId = e.VehiculoId,
            NivelCarga = e.NivelCarga,
            UltimaActualizacion = e.UltimaActualizacion,
            IsActive = e.IsActive,
            CreatedAt = e.CreatedAt
        }).ToList();
        return Ok(estadosDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EstadoBateriaDto>> Get(int id)
    {
        try
        {
            var estado = await _service.GetByIdAsync(id);
            var estadoDto = new EstadoBateriaDto
            {
                Id = estado.Id,
                VehiculoId = estado.VehiculoId,
                NivelCarga = estado.NivelCarga,
                UltimaActualizacion = estado.UltimaActualizacion,
                IsActive = estado.IsActive,
                CreatedAt = estado.CreatedAt
            };
            return Ok(estadoDto);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<EstadoBateriaDto>> Post([FromBody] EstadoBateriaDto estadoDto)
    {
        try
        {
            var estado = new EstadoBateria
            {
                VehiculoId = estadoDto.VehiculoId,
                NivelCarga = estadoDto.NivelCarga,
                UltimaActualizacion = estadoDto.UltimaActualizacion
            };

            await _service.AddAsync(estado);

            estadoDto.Id = estado.Id;
            estadoDto.IsActive = estado.IsActive;
            estadoDto.CreatedAt = estado.CreatedAt;

            return CreatedAtAction(nameof(Get), new { id = estado.Id }, estadoDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] EstadoBateriaDto estadoDto)
    {
        if (id != estadoDto.Id)
            return BadRequest("ID no coincide.");

        var estado = new EstadoBateria
        {
            Id = estadoDto.Id,
            VehiculoId = estadoDto.VehiculoId,
            NivelCarga = estadoDto.NivelCarga,
            UltimaActualizacion = estadoDto.UltimaActualizacion,
            IsActive = estadoDto.IsActive,
            CreatedAt = estadoDto.CreatedAt
        };

        await _service.UpdateAsync(estado);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}