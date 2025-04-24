using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using EvManager.Web.Models;
using EvManager.Domain.Dtos;

namespace EvManager.Web.Controllers
{
    public class PlanRutaController : Controller
    {
        private readonly HttpClient _httpClient;

        public PlanRutaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("EvManagerApi");
        }

       
        public async Task<IActionResult> Index()
        {
            var planes = await _httpClient.GetFromJsonAsync<List<PlanRuta>>("PlanRuta");
            return View(planes ?? new List<PlanRuta>());
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var plan = await _httpClient.GetFromJsonAsync<PlanRuta>($"PlanRuta/{id}");
            if (plan == null)
            {
                return NotFound();
            }
            return View(plan);
        }

       
        public async Task<IActionResult> Create()
        {
            var vehiculos = await _httpClient.GetFromJsonAsync<List<VehiculoDto>>("Vehiculo");
            var estaciones = await _httpClient.GetFromJsonAsync<List<EstacionCargaDto>>("EstacionCarga");

            ViewBag.Vehiculos = vehiculos ?? new List<VehiculoDto>();
            ViewBag.EstacionesCarga = estaciones ?? new List<EstacionCargaDto>();

            return View();
        }

        
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

            
            var vehiculos = await _httpClient.GetFromJsonAsync<List<VehiculoDto>>("Vehiculo");
            var estaciones = await _httpClient.GetFromJsonAsync<List<EstacionCargaDto>>("EstacionCarga");

            ViewBag.Vehiculos = vehiculos ?? new List<VehiculoDto>();
            ViewBag.EstacionesCarga = estaciones ?? new List<EstacionCargaDto>();

            return View(plan);
        }

      
        public async Task<IActionResult> Edit(int id)
        {
            var plan = await _httpClient.GetFromJsonAsync<PlanRuta>($"PlanRuta/{id}");
            if (plan == null)
            {
                return NotFound();
            }
            return View(plan);
        }

       
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

        
        public async Task<IActionResult> Delete(int id)
        {
            var plan = await _httpClient.GetFromJsonAsync<PlanRuta>($"PlanRuta/{id}");
            if (plan == null)
            {
                return NotFound();
            }
            return View(plan);
        }

        
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
}
