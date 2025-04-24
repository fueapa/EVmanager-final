using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using EvManager.Web.Models;

namespace EvManager.Web.Controllers;

public class EstacionCargaController : Controller
{
    private readonly HttpClient _httpClient;

    public EstacionCargaController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("EvManagerApi");
    }

    
    public async Task<IActionResult> Index()
    {
        var estaciones = await _httpClient.GetFromJsonAsync<List<EstacionCarga>>("EstacionCarga");
        return View(estaciones ?? new List<EstacionCarga>());
    }

   
    public async Task<IActionResult> Details(int id)
    {
        var estacion = await _httpClient.GetFromJsonAsync<EstacionCarga>($"EstacionCarga/{id}");
        if (estacion == null)
        {
            return NotFound();
        }
        return View(estacion);
    }

    
    public IActionResult Create()
    {
        return View();
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EstacionCarga estacion)
    {
        if (ModelState.IsValid)
        {
            var response = await _httpClient.PostAsJsonAsync("EstacionCarga", estacion);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al crear la estación de carga.");
        }
        return View(estacion);
    }

   
    public async Task<IActionResult> Edit(int id)
    {
        var estacion = await _httpClient.GetFromJsonAsync<EstacionCarga>($"EstacionCarga/{id}");
        if (estacion == null)
        {
            return NotFound();
        }
        return View(estacion);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EstacionCarga estacion)
    {
        if (id != estacion.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var response = await _httpClient.PutAsJsonAsync($"EstacionCarga/{id}", estacion);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al actualizar la estación de carga.");
        }
        return View(estacion);
    }

    
    public async Task<IActionResult> Delete(int id)
    {
        var estacion = await _httpClient.GetFromJsonAsync<EstacionCarga>($"EstacionCarga/{id}");
        if (estacion == null)
        {
            return NotFound();
        }
        return View(estacion);
    }

   
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _httpClient.DeleteAsync($"EstacionCarga/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Delete), new { id, error = true });
    }
}