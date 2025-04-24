using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using EvManager.Web.Models;

namespace EvManager.Web.Controllers;

public class EstadoBateriaController : Controller
{
    private readonly HttpClient _httpClient;

    public EstadoBateriaController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("EvManagerApi");
    }

    
    public async Task<IActionResult> Index()
    {
        try
        {
            var estados = await _httpClient.GetFromJsonAsync<List<EstadoBateria>>("EstadoBateria");
            return View(estados ?? new List<EstadoBateria>());
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "Error al obtener los estados de batería.");
            return View(new List<EstadoBateria>());
        }
    }

    
    public async Task<IActionResult> Details(int id)
    {
        var estado = await _httpClient.GetFromJsonAsync<EstadoBateria>($"EstadoBateria/{id}");
        if (estado == null)
        {
            return NotFound();
        }
        return View(estado);
    }

   
    public IActionResult Create()
    {
        return View();
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EstadoBateria estado)
    {
        if (ModelState.IsValid)
        {
            var response = await _httpClient.PostAsJsonAsync("EstadoBateria", estado);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al crear el estado de batería.");
        }
        return View(estado);
    }

    
    public async Task<IActionResult> Edit(int id)
    {
        var estado = await _httpClient.GetFromJsonAsync<EstadoBateria>($"EstadoBateria/{id}");
        if (estado == null)
        {
            return NotFound();
        }
        return View(estado);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EstadoBateria estado)
    {
        if (id != estado.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var response = await _httpClient.PutAsJsonAsync($"EstadoBateria/{id}", estado);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al actualizar el estado de batería.");
        }
        return View(estado);
    }

    
    public async Task<IActionResult> Delete(int id, bool error = false)
    {
        var estado = await _httpClient.GetFromJsonAsync<EstadoBateria>($"EstadoBateria/{id}");
        if (estado == null)
        {
            return NotFound();
        }

        if (error)
        {
            ViewBag.Error = "No se pudo eliminar el estado de batería.";
        }

        return View(estado);
    }

    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _httpClient.DeleteAsync($"EstadoBateria/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Delete), new { id, error = true });
    }
}
