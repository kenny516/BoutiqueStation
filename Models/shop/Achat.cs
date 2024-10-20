namespace BoutiqueStation.Models.Shop;

public class Achat
{
    public string Id { get; set; }
    public string IdFournisseur { get; set; }
    public string IdProduit { get; set; }
    public double Quantite { get; set; }
    public double Montant { get; set; }
    public DateOnly Daty { get; set; }= DateOnly.FromDateTime(DateTime.Today);


    public Achat(string id, string idFournisseur, string idProduit, double quantite, double montant, DateOnly daty)
    {
        Id = id;
        IdFournisseur = idFournisseur;
        IdProduit = idProduit;
        Quantite = quantite;
        Montant = montant;
        Daty = daty;
    }

    public Achat()
    {
    }
}