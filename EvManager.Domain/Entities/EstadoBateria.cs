namespace EvManager.Domain.Entities;

public class EstadoBateria
{
    public int Id { get; set; }
    public int VehiculoId { get; set; }
    public Vehiculo Vehiculo { get; set; } = null!; 
    public decimal NivelCarga { get; set; }
    public DateTime UltimaActualizacion { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}