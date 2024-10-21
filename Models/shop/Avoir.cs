namespace BoutiqueStation.Models.shop;

public class Avoir
{
    public String IdVente { get; set; }
    public double Quantite { get; set; }

    public Avoir(string idVente, double quantite)
    {
        IdVente = idVente;
        Quantite = quantite;
    }

    public Avoir()
    {
    }
}