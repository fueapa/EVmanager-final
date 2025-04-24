using Microsoft.EntityFrameworkCore;
using EvManager.Application.Services;
using EvManager.Domain.RepositoryInterfaces;
using EvManager.Infrastructure.Contexts;
using EvManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddDbContext<EvManagerContext>(options =>
    options.UseSqlite("Data Source=evmanager.db"));


builder.Services.AddScoped<IEstacionCargaRepository, EstacionCargaRepository>();
builder.Services.AddScoped<IEstadoBateriaRepository, EstadoBateriaRepository>();
builder.Services.AddScoped<IPlanRutaRepository, PlanRutaRepository>();
builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>(); 


builder.Services.AddScoped<EstacionCargaService>();
builder.Services.AddScoped<EstadoBateriaService>();
builder.Services.AddScoped<PlanRutaService>();
builder.Services.AddScoped<VehiculoService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EvManagerContext>();
    context.Database.EnsureCreated();
}

app.Run();