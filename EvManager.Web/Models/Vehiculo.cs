namespace EvManager.Web.Models;

public class Vehiculo
{
    public int Id { get; set; }
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
    public int CapacidadBateria { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}