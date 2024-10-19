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
        public async Task<JsonResult> List()
        {
            var apiUrl = "https://api.example.com/products"; // Replace with your API endpoint
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var produits = JsonConvert.DeserializeObject<List<Produit>>(jsonString);
                return Json(produits); // Return the list of products as JSON
            }

            // Return an empty list if API call fails
            return Json(new List<Produit>());
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
