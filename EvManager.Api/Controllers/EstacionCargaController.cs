using Microsoft.AspNetCore.Mvc;
using EvManager.Application.Services;
using EvManager.Domain.Entities;
using EvManager.Api.Dtos;

namespace EvManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EstacionCargaController : ControllerBase
{
    private readonly EstacionCargaService _service;

    public EstacionCargaController(EstacionCargaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstacionCargaDto>>> Get()
    {
        var estaciones = await _service.GetAllAsync();
        var estacionesDto = estaciones.Select(e => new EstacionCargaDto
        {
            Id = e.Id,
            Nombre = e.Nombre,
            Ubicacion = e.Ubicacion,
            PuertosDisponibles = e.PuertosDisponibles,
            CapacidadPotencia = e.CapacidadPotencia,
            IsActive = e.IsActive,
            CreatedAt = e.CreatedAt
        }).ToList();
        return Ok(estacionesDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EstacionCargaDto>> Get(int id)
    {
        var estacion = await _service.GetByIdAsync(id);
        if (estacion == null)
            return NotFound($"Estación de carga con ID {id} no encontrada.");

        var estacionDto = new EstacionCargaDto
        {
            Id = estacion.Id,
            Nombre = estacion.Nombre,
            Ubicacion = estacion.Ubicacion,
            PuertosDisponibles = estacion.PuertosDisponibles,
            CapacidadPotencia = estacion.CapacidadPotencia,
            IsActive = estacion.IsActive,
            CreatedAt = estacion.CreatedAt
        };
        return Ok(estacionDto);
    }

    [HttpPost]
    public async Task<ActionResult<EstacionCargaDto>> Post([FromBody] EstacionCargaDto estacionDto)
    {
        try
        {
            var estacion = new EstacionCarga
            {
                Nombre = estacionDto.Nombre,
                Ubicacion = estacionDto.Ubicacion,
                PuertosDisponibles = estacionDto.PuertosDisponibles,
                CapacidadPotencia = estacionDto.CapacidadPotencia
            };

            await _service.AddAsync(estacion);

            estacionDto.Id = estacion.Id;
            estacionDto.IsActive = estacion.IsActive;
            estacionDto.CreatedAt = estacion.CreatedAt;

            return CreatedAtAction(nameof(Get), new { id = estacion.Id }, estacionDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] EstacionCargaDto estacionDto)
    {
        if (id != estacionDto.Id)
            return BadRequest("ID no coincide.");

        var estacion = new EstacionCarga
        {
            Id = estacionDto.Id,
            Nombre = estacionDto.Nombre,
            Ubicacion = estacionDto.Ubicacion,
            PuertosDisponibles = estacionDto.PuertosDisponibles,
            CapacidadPotencia = estacionDto.CapacidadPotencia,
            IsActive = estacionDto.IsActive,
            CreatedAt = estacionDto.CreatedAt
        };

        await _service.UpdateAsync(estacion);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}