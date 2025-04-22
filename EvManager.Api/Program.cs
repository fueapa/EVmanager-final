using Microsoft.EntityFrameworkCore;
using EvManager.Application.Services;
using EvManager.Domain.RepositoryInterfaces;
using EvManager.Infrastructure.Contexts;
using EvManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure SQLite database context
builder.Services.AddDbContext<EvManagerContext>(options =>
    options.UseSqlite("Data Source=evmanager.db"));

// Register repositories
builder.Services.AddScoped<IEstacionCargaRepository, EstacionCargaRepository>();
builder.Services.AddScoped<IEstadoBateriaRepository, EstadoBateriaRepository>();
builder.Services.AddScoped<IPlanRutaRepository, PlanRutaRepository>();
builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>(); // Nueva línea

// Register services
builder.Services.AddScoped<EstacionCargaService>();
builder.Services.AddScoped<EstadoBateriaService>();
builder.Services.AddScoped<PlanRutaService>();
builder.Services.AddScoped<VehiculoService>();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Ensure database is created and migrated
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EvManagerContext>();
    context.Database.EnsureCreated();
}

app.Run();