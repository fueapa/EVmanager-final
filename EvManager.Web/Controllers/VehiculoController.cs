using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using EvManager.Web.Models;

namespace EvManager.Web.Controllers;

public class VehiculoController : Controller
{
    private readonly HttpClient _httpClient;

    public VehiculoController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("EvManagerApi");
    }

   
    public async Task<IActionResult> Index()
    {
        var vehiculos = await _httpClient.GetFromJsonAsync<List<Vehiculo>>("Vehiculo");
        return View(vehiculos ?? new List<Vehiculo>());
    }

    
    public async Task<IActionResult> Details(int id)
    {
        var vehiculo = await _httpClient.GetFromJsonAsync<Vehiculo>($"Vehiculo/{id}");
        if (vehiculo == null)
        {
            return NotFound();
        }
        return View(vehiculo);
    }

   
    public IActionResult Create()
    {
        return View();
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Vehiculo vehiculo)
    {
        if (ModelState.IsValid)
        {
            var response = await _httpClient.PostAsJsonAsync("Vehiculo", vehiculo);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al crear el vehículo.");
        }
        return View(vehiculo);
    }

    
    public async Task<IActionResult> Edit(int id)
    {
        var vehiculo = await _httpClient.GetFromJsonAsync<Vehiculo>($"Vehiculo/{id}");
        if (vehiculo == null)
        {
            return NotFound();
        }
        return View(vehiculo);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Vehiculo vehiculo)
    {
        if (id != vehiculo.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var response = await _httpClient.PutAsJsonAsync($"Vehiculo/{id}", vehiculo);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al actualizar el vehículo.");
        }
        return View(vehiculo);
    }

   
    public async Task<IActionResult> Delete(int id)
    {
        var vehiculo = await _httpClient.GetFromJsonAsync<Vehiculo>($"Vehiculo/{id}");
        if (vehiculo == null)
        {
            return NotFound();
        }
        return View(vehiculo);
    }

  
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _httpClient.DeleteAsync($"Vehiculo/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Delete), new { id, error = true });
    }
}