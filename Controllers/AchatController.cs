using BoutiqueStation.Models.Shop;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BoutiqueStation.Controllers;

public class AchatController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly FournisseurController _fournisseurController;
    private readonly ProduitController _produitController;

    public AchatController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _fournisseurController = new FournisseurController(httpClientFactory.CreateClient());
        _produitController = new ProduitController(httpClientFactory.CreateClient());
    }
    
    
    [HttpGet]
    private async Task<List<Achat>> List()
    {
        const string apiUrl = "http://localhost:8080/station/achat";
        var client = _httpClientFactory.CreateClient();

        try
        {
            var response = await client.GetStringAsync(apiUrl);
            var achats = JsonConvert.DeserializeObject<List<Achat>>(response);
            return achats;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Error calling API: {ex.Message}");
        }
    }
    
    
    
    [HttpGet("list")]
    public async Task<IActionResult> ListAchat()
    {
        try
        {
            // Fetch ventes from the API
            var achats = await List();

            // Return the ListeVente view with the retrieved data
            return View("~/Views/shop/achat/ListeAchat.cshtml", achats);
        }
        catch (Exception ex)
        {
            // Handle any errors that occurred while fetching the ventes
            return StatusCode(500, ex.Message);
        }
    }


    // GET
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var fournisseurs = await _fournisseurController.List();
        var produits = await _produitController.List();
        ViewData["Fournisseurs"] = fournisseurs;
        ViewData["Produits"] = produits;
        
        return View("~/Views/shop/achat/InsertionAchat.cshtml", new Achat());
    }

    [HttpPost]
    public IActionResult Create(Achat achat)
    {
        
        
        
        return RedirectToAction("Index");
    }
    
    public IActionResult Index()
    {
        return View("~/Views/shop/achat/ListeAchat.cshtml"); // Placeholder, implement listing of ventes here
    }
}