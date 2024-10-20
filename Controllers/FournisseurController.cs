using BoutiqueStation.Models.Shop;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BoutiqueStation.Controllers;

public class FournisseurController : Controller
{
    private readonly HttpClient _httpClient;

    public FournisseurController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    

    public async Task<List<Fournisseur>> List()
    {
        const string apiUrl = "http://localhost:8080/station/fournissuers";
        var response = await _httpClient.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var clients = JsonConvert.DeserializeObject<List<Fournisseur>>(jsonString);
            return clients ?? [];
        }

        // Return an empty list if API call fails
        return [];
    }
}