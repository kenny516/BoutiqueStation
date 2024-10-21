using System.Text.Json;
using BoutiqueStation.Models.shop;
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
            var clients = await _clientController.List();
            var produits = await _produitController.List();

            var vente = new Vente
            {
                Client = new Client(),
                Produit = new Produit()
            };
            ViewBag.Clients = clients;
            ViewBag.Produits = produits;

            return View("~/Views/shop/vente/InsertionVente.cshtml", vente);
        }

        public async Task<IActionResult> CreateAvoir(String id, double quantite)
        {
            var clients = await _clientController.List();
            var produits = await _produitController.List();

            var avoir = new Avoir
            {
                IdVente = id,
                Quantite = quantite
            };
            ViewBag.Clients = clients;
            ViewBag.Produits = produits;

            return View("~/Views/shop/vente/ModifCmd.cshtml", avoir);
        }

        
        [HttpPost("createAvoir")]
        public async Task<IActionResult> CreateAvoirForm(Avoir avoir)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var requestUri = "http://localhost:8080/station/avoir"; // Update with your actual Java API endpoint

                // Create form data to send in the POST request
                var postData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("idVente", avoir.IdVente),
                    new KeyValuePair<string, string>("quantite", avoir.Quantite.ToString()),
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
                Console.WriteLine(ex.Message + Json(avoir));
                return View(
                    "~/Views/shop/vente/Error.cshtml");
            }
        }

        // POST: Vente/Create
        [HttpPost]
        public async Task<IActionResult> Create(Vente vente)
        {
            try
            {
                var bonValue = Request.Form["bonValue"];
                var client = _httpClientFactory.CreateClient();
                var requestUri = "http://localhost:8080/station/vente";

                // Create form data to send in the POST request
                var postData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("isBon", bonValue),
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
                var idVente = ExtractIdVenteFromResponse(responseContent);
                vente.Id = idVente;
                ViewBag.Message = responseContent;
                ViewBag.vente = vente;

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
        }
        private string? ExtractIdVenteFromResponse(string jsonResponse)
        {
            // Deserialize the JSON response to a dynamic object or a specific class
            using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
            {
                if (doc.RootElement.TryGetProperty("idVente", out JsonElement idElement))
                {
                    return idElement.GetString();
                }
                else
                {
                    throw new Exception("idVente not found in the response");
                }
            }
        }


        public IActionResult Index()
        {
            return View(); // Placeholder, implement listing of ventes here
        }
    }
}