using Microsoft.AspNetCore.Mvc;
using EvManager.Application.Services;
using EvManager.Domain.Entities;
using EvManager.Api.Dtos;

namespace EvManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlanRutaController : ControllerBase
{
    private readonly PlanRutaService _service;

    public PlanRutaController(PlanRutaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlanRutaDto>>> Get()
    {
        var planes = await _service.GetAllAsync();
        var planesDto = planes.Select(p => new PlanRutaDto
        {
            Id = p.Id,
            VehiculoId = p.VehiculoId,
            UbicacionInicio = p.UbicacionInicio,
            UbicacionFin = p.UbicacionFin,
            DistanciaEstimada = p.DistanciaEstimada,
            IdsEstacionesCarga = p.IdsEstacionesCarga,
            IsActive = p.IsActive,
            CreatedAt = p.CreatedAt
        }).ToList();
        return Ok(planesDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlanRutaDto>> Get(int id)
    {
        try
        {
            var plan = await _service.GetByIdAsync(id);
            var planDto = new PlanRutaDto
            {
                Id = plan.Id,
                VehiculoId = plan.VehiculoId,
                UbicacionInicio = plan.UbicacionInicio,
                UbicacionFin = plan.UbicacionFin,
                DistanciaEstimada = plan.DistanciaEstimada,
                IdsEstacionesCarga = plan.IdsEstacionesCarga,
                IsActive = plan.IsActive,
                CreatedAt = plan.CreatedAt
            };
            return Ok(planDto);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<PlanRutaDto>> Post([FromBody] PlanRutaDto planDto)
    {
        try
        {
            var plan = new PlanRuta
            {
                VehiculoId = planDto.VehiculoId,
                UbicacionInicio = planDto.UbicacionInicio,
                UbicacionFin = planDto.UbicacionFin,
                DistanciaEstimada = planDto.DistanciaEstimada,
                IdsEstacionesCarga = planDto.IdsEstacionesCarga
            };

            await _service.AddAsync(plan);

            planDto.Id = plan.Id;
            planDto.IsActive = plan.IsActive;
            planDto.CreatedAt = plan.CreatedAt;

            return CreatedAtAction(nameof(Get), new { id = plan.Id }, planDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] PlanRutaDto planDto)
    {
        if (id != planDto.Id)
            return BadRequest("ID no coincide.");

        var plan = new PlanRuta
        {
            Id = planDto.Id,
            VehiculoId = planDto.VehiculoId,
            UbicacionInicio = planDto.UbicacionInicio,
            UbicacionFin = planDto.UbicacionFin,
            DistanciaEstimada = planDto.DistanciaEstimada,
            IdsEstacionesCarga = planDto.IdsEstacionesCarga,
            IsActive = planDto.IsActive,
            CreatedAt = planDto.CreatedAt
        };

        await _service.UpdateAsync(plan);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}