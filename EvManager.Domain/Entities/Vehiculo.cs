namespace EvManager.Domain.Entities;

public class Vehiculo
{
    public int Id { get; set; }
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
    public int CapacidadBateria { get; set; } // En kWh
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}