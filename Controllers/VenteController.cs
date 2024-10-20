using BoutiqueStation.Models.Shop; // Adjust this to match your actual namespace
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace BoutiqueStation.Controllers
{
    public class VenteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ClientController _clientController;
        private readonly ProduitController _produitController;

        // Constructor using IHttpClientFactory to create HttpClient
        public VenteController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _clientController = new ClientController(httpClientFactory.CreateClient());
            _produitController = new ProduitController(httpClientFactory.CreateClient());
        }

        // Method to fetch list of ventes from the API
        [HttpGet]
        private async Task<List<Vente>> List()
        {
            const string apiUrl = "http://localhost:8080/station/vente";
            var client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetStringAsync(apiUrl);
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
            // Fetch clients and products
            var clients = await _clientController.List();
            var produits = await _produitController.List();

            // Ensure lists are not null

            // Create a new Vente object
            var vente = new Vente
            {
                Client = new Client(), // Ensure Client is initialized
                Produit = new Produit() // Ensure Produit is initialized
            };

            // Set ViewBag properties
            ViewBag.Clients = clients;
            ViewBag.Produits = produits;

            return View("~/Views/shop/vente/InsertionVente.cshtml", vente);
        }


        // POST: Vente/Create
        [HttpPost]
        public async Task<IActionResult> Create(Vente vente)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var requestUri = "http://localhost:8080/station/vente"; // Update with your actual Java API endpoint

                // Create form data to send in the POST request
                var postData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("magasin", vente.IdMagasin),
                    new KeyValuePair<string, string>("produit",
                        vente.Produit.Id), // Ensure this matches your API's expected key
                    new KeyValuePair<string, string>("quantite", vente.Quantite.ToString()),
                    new KeyValuePair<string, string>("etat", "14"), // Assuming a fixed value for "etat"
                    new KeyValuePair<string, string>("client", vente.Client.Id),
                    new KeyValuePair<string, string>("date",
                        vente.Daty.ToString("yyyy-MM-dd")) // Ensure the date format is correct
                });


                // Send POST request
                var response = await client.PostAsync(requestUri, postData);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                // Handle the response from the Java API
                ViewBag.Message = responseContent;

                return
                    View(
                        "~/Views/shop/vente/Confirmation.cshtml"); // Redirect to confirmation view or any other appropriate view
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the request
                ViewBag.err = ex.Message;
                // ViewBag.err = vente;
                ViewBag.verif = ModelState.ErrorCount;
                Console.WriteLine(ex.Message + Json(vente));
                return View(
                    "~/Views/shop/vente/Error.cshtml");
            }
            // var clients = await _clientController.List(); // Use the right method here
            // var produits = await _produitController.List(); // Use the right method here
            // ViewBag.Clients = clients;
            // ViewBag.Produits = produits;

            // return  View("~/Views/shop/vente/Error.cshtml");// Return the same view with validation errors
        }

        // // POST: Vente/Verif
        // [HttpPost]
        
        
        
        
        
        // Optional: Define an Index action for listing Vente entries
        public IActionResult Index()
        {
            return View(); // Placeholder, implement listing of ventes here
        }
    }
}