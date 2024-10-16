namespace BoutiqueStation.Models.Shop;

public class Client
{
    public string Id { get; set; }
    public string nom { get; set; }
    
    public Client(string id, string nom)
    {
        Id = id;
        this.nom = nom;
    }
    public Client()
    {
    }
}