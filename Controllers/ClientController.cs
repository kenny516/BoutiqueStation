using BoutiqueStation.Models.Shop;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BoutiqueStation.Controllers
{
    public class ClientController : Controller
    {
        private readonly HttpClient _httpClient;

        public ClientController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Client/List
        public async Task<List<Client>> List()
        {
            const string apiUrl = "http://localhost:8080/station/client"; // Replace with your API endpoint URL
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var clients = JsonConvert.DeserializeObject<List<Client>>(jsonString);
                return clients ?? [];
            }

            // Return an empty list if API call fails
            return [];
        }
    }
}
