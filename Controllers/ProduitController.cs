using BoutiqueStation.Models.Shop;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BoutiqueStation.Controllers
{
    public class ProduitController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProduitController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Produit/List
        public async Task<List<Produit>> List()
        {
            var apiUrl = "http://localhost:8080/station/produit"; // Replace with your API endpoint
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var produits = JsonConvert.DeserializeObject<List<Produit>>(jsonString);
        
                // Use the null-coalescing operator to return an empty list if produits is null
                return produits ?? [];
            }

            // Log an error or throw an exception if needed
            Console.WriteLine($"API call failed with status code: {response.StatusCode}");
    
            // Return an empty list if API call fails
            return [];
        }

    }
}
// [
// {
//     "id": "P001",
//     "val": "Gasoline",
//     "desce": "High-quality gasoline for vehicles",
//     "puAchat": 60.50,
//     "puVente": 70.00
// },
// {
//     "id": "P002",
//     "val": "Diesel",
//     "desce": "Premium diesel fuel",
//     "puAchat": 55.00,
//     "puVente": 65.00
// },
// {
//     "id": "P003",
//     "val": "Motor Oil",
//     "desce": "Synthetic motor oil",
//     "puAchat": 20.00,
//     "puVente": 25.00
// }
// ]
