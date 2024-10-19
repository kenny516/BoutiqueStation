using BoutiqueStation.Models.Shop; // Adjust this according to your actual namespace
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace BoutiqueStation.Controllers
{
    public class VenteController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ClientController _clientController;

        public VenteController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _clientController = new ClientController(httpClient);
        }

        // Method to fetch list of ventes
        private async Task<List<Vente>> List()
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

        // GET: api/vente/list
        [HttpGet("list")]
        public async Task<IActionResult> ListVente()
        {
            try
            {
                // Fetch ventes from the API
                var ventes = await List();

                // Return the ListeVente view with the retrieved data
                return View("~/Views/shop/vente/ListeVente.cshtml", ventes);
            }
            catch (Exception ex)
            {
                // Handle any errors that occurred while fetching the ventes
                return StatusCode(500, ex.Message);
            }
        }

        // GET: Vente/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Call ClientController to get the list of clients
            var clients = await _clientController.List(); // Ensure you're using the right method

            ViewBag.Clients = clients; // Pass the clients list to the view

            return View("~/Views/shop/vente/InsertionVente.cshtml", new Vente()); // Return the form view with a new Vente instance
        }

        // POST: Vente/Create
        [HttpPost]
        public async Task<IActionResult> Create(Vente vente)
        {
            if (ModelState.IsValid)
            {
                // Handle form submission logic here (save Vente)
                // You may want to send the `vente` object to an API for saving
                // Example: await _httpClient.PostAsJsonAsync("http://localhost:8080/api/vente", vente);

                // For now, redirect to an index page or confirmation
                return RedirectToAction("Index");
            }

            // If model validation fails, return the form with the client list again
            var clients = await _clientController.List(); // Use the right method here
            ViewBag.Clients = clients;
            return View("~/Views/shop/vente/InsertionVente.cshtml", vente); // Return the same view with validation errors
        }

        // Optional: Define an Index action for listing Vente entries
        public IActionResult Index()
        {
            return View(); // Placeholder, implement listing of ventes here
        }
    }
}
