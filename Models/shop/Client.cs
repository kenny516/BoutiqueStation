namespace BoutiqueStation.Models.Shop;

public class Client
{
    public string Id { get; set; }
    public string Nom { get; set; }
    
    public Client(string id, string nom)
    {
        Id = id;
        this.Nom = nom;
    }
    public Client()
    {
    }
}