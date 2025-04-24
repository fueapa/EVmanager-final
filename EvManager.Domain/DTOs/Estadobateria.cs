namespace EvManager.Domain.Dtos;

public class EstadoBateriaDto
{
    public int Id { get; set; }
    public int VehiculoId { get; set; }
    public decimal NivelCarga { get; set; }
    public DateTime UltimaActualizacion { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}