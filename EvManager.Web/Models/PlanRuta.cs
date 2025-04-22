namespace EvManager.Web.Models;

public class PlanRuta
{
    public int Id { get; set; }
    public int VehiculoId { get; set; }
    public string UbicacionInicio { get; set; } = string.Empty;
    public string UbicacionFin { get; set; } = string.Empty;
    public decimal DistanciaEstimada { get; set; }
    public List<int> IdsEstacionesCarga { get; set; } = new List<int>();
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}