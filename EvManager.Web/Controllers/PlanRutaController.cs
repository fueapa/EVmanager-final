using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using EvManager.Web.Models;

namespace EvManager.Web.Controllers;

public class PlanRutaController : Controller
{
    private readonly HttpClient _httpClient;

    public PlanRutaController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("EvManagerApi");
    }

    // GET: PlanRuta
    public async Task<IActionResult> Index()
    {
        var planes = await _httpClient.GetFromJsonAsync<List<PlanRuta>>("PlanRuta");
        return View(planes ?? new List<PlanRuta>());
    }

    // GET: PlanRuta/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var plan = await _httpClient.GetFromJsonAsync<PlanRuta>($"PlanRuta/{id}");
        if (plan == null)
        {
            return NotFound();
        }
        return View(plan);
    }

    // GET: PlanRuta/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PlanRuta/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PlanRuta plan)
    {
        if (ModelState.IsValid)
        {
            var response = await _httpClient.PostAsJsonAsync("PlanRuta", plan);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al crear el plan de ruta.");
        }
        return View(plan);
    }

    // GET: PlanRuta/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var plan = await _httpClient.GetFromJsonAsync<PlanRuta>($"PlanRuta/{id}");
        if (plan == null)
        {
            return NotFound();
        }
        return View(plan);
    }

    // POST: PlanRuta/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PlanRuta plan)
    {
        if (id != plan.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var response = await _httpClient.PutAsJsonAsync($"PlanRuta/{id}", plan);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al actualizar el plan de ruta.");
        }
        return View(plan);
    }

    // GET: PlanRuta/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var plan = await _httpClient.GetFromJsonAsync<PlanRuta>($"PlanRuta/{id}");
        if (plan == null)
        {
            return NotFound();
        }
        return View(plan);
    }

    // POST: PlanRuta/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _httpClient.DeleteAsync($"PlanRuta/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Delete), new { id, error = true });
    }
}