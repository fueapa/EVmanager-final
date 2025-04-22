using Microsoft.EntityFrameworkCore;
using EvManager.Domain.Entities;

namespace EvManager.Infrastructure.Contexts;

public class EvManagerContext : DbContext
{
    public EvManagerContext(DbContextOptions<EvManagerContext> options) : base(options) { }

    public DbSet<EstacionCarga> EstacionesCarga { get; set; }
    public DbSet<EstadoBateria> EstadosBateria { get; set; }
    public DbSet<PlanRuta> PlanesRuta { get; set; }
    public DbSet<Vehiculo> Vehiculos { get; set; } // Nueva entidad

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=evmanager.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EstacionCarga>()
            .Property(ec => ec.CapacidadPotencia)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<EstadoBateria>()
            .Property(eb => eb.NivelCarga)
            .HasColumnType("decimal(5,2)");

        modelBuilder.Entity<PlanRuta>()
            .Property(pr => pr.DistanciaEstimada)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Vehiculo>()
            .Property(v => v.CapacidadBateria)
            .HasColumnType("integer");

        // Configurar relaciones
        modelBuilder.Entity<EstadoBateria>()
            .HasOne(eb => eb.Vehiculo)
            .WithMany()
            .HasForeignKey(eb => eb.VehiculoId);

        modelBuilder.Entity<PlanRuta>()
            .HasOne(pr => pr.Vehiculo)
            .WithMany()
            .HasForeignKey(pr => pr.VehiculoId);
    }
}