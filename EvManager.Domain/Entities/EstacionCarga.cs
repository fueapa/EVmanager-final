using EvManager.Domain.Core;

namespace EvManager.Domain.Entities;

public class EstacionCarga : BaseEntity
{
    public string Nombre { get; set; }
    public string Ubicacion { get; set; }
    public int PuertosDisponibles { get; set; }
    public decimal CapacidadPotencia { get; set; }

}