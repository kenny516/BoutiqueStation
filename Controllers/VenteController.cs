using BoutiqueStation.Models.Shop; // Adjust this according to your actual namespace
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BoutiqueStation.Controllers
{
    public class VenteController : Controller
    {
        private readonly HttpClient _httpClient;

        public VenteController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task<List<Vente>> GetVentesFromApi()
        {
            const string apiUrl = "http://localhost:8080/api/vente";

            try
            {
                var response = await _httpClient.GetStringAsync(apiUrl);

                var ventes = JsonConvert.DeserializeObject<List<Vente>>(response);

                return ventes;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error calling API: {ex.Message}");
            }
        }

        // GET: api/vente/liste
        [HttpGet("liste")]
        public async Task<IActionResult> ListeVente()
        {
            try
            {
                // Fetch ventes from the API
                var ventes = await GetVentesFromApi();

                // Return the ListeVente view from the shop/vente subdirectory with the retrieved data
                return View("~/Views/shop/vente/ListeVente.cshtml", ventes);
            }
            catch (Exception ex)
            {
                // Handle any errors that occurred while fetching the ventes
                return StatusCode(500, ex.Message);
            }
        }

        // Action to display the insertion form
        [HttpGet]
        public IActionResult InsertionVente()
        {
            // call liste produit

            // Return the InsertionVente view from the shop/vente subdirectory
            return View("~/Views/shop/vente/InsertionVente.cshtml");
        }
    }
}