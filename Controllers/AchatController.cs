using BoutiqueStation.Models.Shop;
using Microsoft.AspNetCore.Mvc;

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
        // Logic to save the achat to the database
        
        return RedirectToAction("Index");
    }
    
    public IActionResult Index()
    {
        return View("~/Views/shop/achat/ListeAchat.cshtml"); // Placeholder, implement listing of ventes here
    }
}