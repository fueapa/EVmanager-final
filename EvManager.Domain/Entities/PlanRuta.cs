namespace EvManager.Domain.Entities;

public class PlanRuta
{
    public int Id { get; set; }
    public int VehiculoId { get; set; }
    public Vehiculo Vehiculo { get; set; } = null!; // Relación de navegación
    public string UbicacionInicio { get; set; } = string.Empty;
    public string UbicacionFin { get; set; } = string.Empty;
    public decimal DistanciaEstimada { get; set; }
    public List<int> IdsEstacionesCarga { get; set; } = new List<int>();
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}