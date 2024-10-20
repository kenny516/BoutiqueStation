namespace BoutiqueStation.Models.shop;

public class EtatStock
{
    public string Id { get; set; }
    public string IdProduitLib { get; set; }
    public string IdTypeProduit { get; set; }
    public string IdTypeProduitLib { get; set; }
    public string IdMagasin { get; set; }
    public string IdMagasinLib { get; set; }
    public DateTime DateDernierInventaire { get; set; }
    public double Quantite { get; set; }
    public double Entree { get; set; }
    public double Sortie { get; set; }
    public double Reste { get; set; }
    public double PuVente { get; set; }
    public string IdUnite { get; set; }
    public string IdUniteLib { get; set; }

    // Constructeur vide
    public EtatStock()
    {
    }

    // Constructeur avec tous les arguments
    public EtatStock(string id, string idProduitLib, string idTypeProduit, string idTypeProduitLib, string idMagasin,
        string idMagasinLib, DateTime dateDernierInventaire, double quantite, double entree, double sortie,
        double reste, double puVente, string idUnite, string idUniteLib)
    {
        Id = id;
        IdProduitLib = idProduitLib;
        IdTypeProduit = idTypeProduit;
        IdTypeProduitLib = idTypeProduitLib;
        IdMagasin = idMagasin;
        IdMagasinLib = idMagasinLib;
        DateDernierInventaire = dateDernierInventaire;
        Quantite = quantite;
        Entree = entree;
        Sortie = sortie;
        Reste = reste;
        PuVente = puVente;
        IdUnite = idUnite;
        IdUniteLib = idUniteLib;
    }
}