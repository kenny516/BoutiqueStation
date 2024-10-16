using BoutiqueStation.Models.Shop;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BoutiqueStation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenteController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public VenteController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetVente()
        {
            // Example API URL (replace with your actual Java API endpoint)
            const string apiUrl = "http://localhost:8080/api/vente"; // Adjust the URL as needed

            try
            {
                // Call the Java API
                var response = await _httpClient.GetStringAsync(apiUrl);

                // Deserialize JSON response into the Vente object
                var vente = JsonConvert.DeserializeObject<Vente>(response);

                // Return the object as JSON
                return Ok(vente);
            }
            catch (HttpRequestException ex)
            {
                // Handle any errors when calling the API
                return StatusCode(500, $"Error calling API: {ex.Message}");
            }
        }
    }
}