namespace EvManager.Domain.Dtos;

public class EstacionCargaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Ubicacion { get; set; } = string.Empty;
    public int PuertosDisponibles { get; set; }
    public decimal CapacidadPotencia { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}