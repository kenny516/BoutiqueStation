namespace BoutiqueStation.Models.Shop
{
    public class Vente
    {
        public string Id { get; set; }
        public string Designation { get; set; }
        
        public string IdMagasin { get; set; }
        public DateOnly Daty { get; set; }
        public string IdClient { get; set; }
        
        public Vente()
        {
            IdMagasin = "PHARM001";
        }
        
        public Vente(string id, string designation, string idClient, DateOnly daty)
        {
            Id = id;
            Designation = designation;
            IdMagasin = "PHARM001";
            Daty = daty;
            IdClient = idClient;
        }
    }
}