namespace BoutiqueStation.Models.Shop
{
    public class Produit
    {
        public string Id { get; set; }
        public string Val { get; set; }
        public string Desce { get; set; }
        public double PuAchat { get; set; }
        public double PuVente { get; set; }

        // Constructor with parameters
        public Produit(string id, string val, string desce, double puAchat, double puVente)
        {
            Id = id;
            Val = val;
            Desce = desce;
            PuAchat = puAchat;
            PuVente = puVente;
        }

        public Produit()
        {
        }
    }
}