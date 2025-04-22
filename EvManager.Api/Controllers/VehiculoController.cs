using Microsoft.AspNetCore.Mvc;
using EvManager.Application.Services;
using EvManager.Domain.Entities;
using EvManager.Api.Dtos;

namespace EvManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehiculoController : ControllerBase
{
    private readonly VehiculoService _service;

    public VehiculoController(VehiculoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehiculoDto>>> Get()
    {
        var vehiculos = await _service.GetAllAsync();
        var vehiculosDto = vehiculos.Select(v => new VehiculoDto
        {
            Id = v.Id,
            Marca = v.Marca,
            Modelo = v.Modelo,
            Matricula = v.Matricula,
            CapacidadBateria = v.CapacidadBateria,
            IsActive = v.IsActive,
            CreatedAt = v.CreatedAt
        }).ToList();
        return Ok(vehiculosDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VehiculoDto>> Get(int id)
    {
        var vehiculo = await _service.GetByIdAsync(id);
        if (vehiculo == null)
            return NotFound($"Vehículo con ID {id} no encontrado.");

        var vehiculoDto = new VehiculoDto
        {
            Id = vehiculo.Id,
            Marca = vehiculo.Marca,
            Modelo = vehiculo.Modelo,
            Matricula = vehiculo.Matricula,
            CapacidadBateria = vehiculo.CapacidadBateria,
            IsActive = vehiculo.IsActive,
            CreatedAt = vehiculo.CreatedAt
        };
        return Ok(vehiculoDto);
    }

    [HttpPost]
    public async Task<ActionResult<VehiculoDto>> Post([FromBody] VehiculoDto vehiculoDto)
    {
        try
        {
            var vehiculo = new Vehiculo
            {
                Marca = vehiculoDto.Marca,
                Modelo = vehiculoDto.Modelo,
                Matricula = vehiculoDto.Matricula,
                CapacidadBateria = vehiculoDto.CapacidadBateria
            };

            await _service.AddAsync(vehiculo);

            vehiculoDto.Id = vehiculo.Id;
            vehiculoDto.IsActive = vehiculo.IsActive;
            vehiculoDto.CreatedAt = vehiculo.CreatedAt;

            return CreatedAtAction(nameof(Get), new { id = vehiculo.Id }, vehiculoDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] VehiculoDto vehiculoDto)
    {
        if (id != vehiculoDto.Id)
            return BadRequest("ID no coincide.");

        var vehiculo = new Vehiculo
        {
            Id = vehiculoDto.Id,
            Marca = vehiculoDto.Marca,
            Modelo = vehiculoDto.Modelo,
            Matricula = vehiculoDto.Matricula,
            CapacidadBateria = vehiculoDto.CapacidadBateria,
            IsActive = vehiculoDto.IsActive,
            CreatedAt = vehiculoDto.CreatedAt
        };

        await _service.UpdateAsync(vehiculo);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}