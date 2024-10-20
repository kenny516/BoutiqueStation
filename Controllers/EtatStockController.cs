using BoutiqueStation.Models.shop;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BoutiqueStation.Controllers;

public class EtatStockController :Controller
{
    private readonly HttpClient _httpClient;

    public EtatStockController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    // GET: EtatStock/List
    public async Task<List<EtatStock>> List()
    {
        const string apiUrl = "http://localhost:8080/station/etatStock"; // Replace with your API endpoint URL
        var response = await _httpClient.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var etatStocks = JsonConvert.DeserializeObject<List<EtatStock>>(jsonString);
            return etatStocks ?? [];
        }

        // Return an empty list if API call fails
        return [];
    }
    
    public async Task<IActionResult> ListEtatStock()
    {
        try
        {
            // Fetch ventes from the API
            var etatStocks = await List();

            // Return the ListeVente view with the retrieved data
            return View("~/Views/shop/stock/ListeStock.cshtml", etatStocks);
        }
        catch (Exception ex)
        {
            // Handle any errors that occurred while fetching the ventes
            return StatusCode(500, ex.Message);
        }
    }
    
}