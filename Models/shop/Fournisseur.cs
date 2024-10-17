namespace BoutiqueStation.Models.Shop;

public class Fournisseur
{
    public string Id { get; set; }
    public string Nom { get; set; }

    public Fournisseur(string id, string nom)
    {
        Id = id;
        Nom = nom;
    }

    public Fournisseur()
    {
    }
}