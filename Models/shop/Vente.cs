namespace BoutiqueStation.Models.Shop
{
    public class Vente
    {
        public string Id { get; set; }
        public string Designation { get; set; }
        public string IdMagasin { get; set; }

        public double Quantite { get; set; }
        public double Montant { get; set; }
        public DateOnly Daty { get; set; }
        public string IdClient { get; set; }

        public Produit Produit { get; set; }
        public Client Client { get; set; }

        public Vente()
        {
            IdMagasin = "PHARM001";
        }

        public Vente(string id, string designation, string idMagasin, double quantite, double montant, DateOnly daty, string idClient, Produit produit, Client client)
        {
            Id = id;
            Designation = designation;
            IdMagasin = idMagasin;
            Quantite = quantite;
            Montant = montant;
            Daty = daty;
            IdClient = idClient;
            Produit = produit;
            Client = client;
        }
    }
}