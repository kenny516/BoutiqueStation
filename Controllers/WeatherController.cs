using BoutiqueStation.Models;

namespace BoutiqueStation.Controllers;

using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class WeatherController : Controller
{
    private readonly HttpClient _httpClient;

    public WeatherController()
    {
        _httpClient = new HttpClient();
    }

    public async Task<IActionResult> Index()
    {
        // Example API URL (you can replace this with any API you're using)
        string apiUrl = "https://api.openweathermap.org/data/2.5/weather?q=London&appid=your_api_key";

        // Call the API and get the response
        var response = await _httpClient.GetStringAsync(apiUrl);

        // Deserialize JSON response into the model
        var weatherData = JsonConvert.DeserializeObject<WeatherResponse>(response);

        // Pass the data to the view
        return View(weatherData);
    }
}

// Create a response model to deserialize JSON
public class WeatherResponse
{
    public List<Weather> Weather { get; set; }
}
