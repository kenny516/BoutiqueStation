using System.ComponentModel.DataAnnotations;

namespace BoutiqueStation.Models.Shop
{
    public class Vente
    {
        [Key]
        public string? Id { get; set; }

        [Required(ErrorMessage = "La désignation est requise.")]
        [StringLength(100, ErrorMessage = "La désignation ne peut pas dépasser 100 caractères.")]
        public string Designation { get; set; }

        public string IdMagasin { get; set; }

        [Required(ErrorMessage = "La quantité est requise.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "La quantité doit être supérieure à 0.")]
        public double Quantite { get; set; }
        
        public double Montant { get; set; }

        public DateOnly Daty { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        public Produit Produit { get; set; }

        public Client Client { get; set; }

        public Vente()
        {
            IdMagasin = "MAG000245";
        }

        public Vente(string? id, string designation, string idMagasin, double quantite, double montant, DateOnly daty, string idClient, Produit produit, Client client)
        {
            IdMagasin = "MAG000245";
            Id = id;
            Designation = designation;
            IdMagasin = idMagasin;
            Quantite = quantite;
            Montant = montant;
            Daty = daty;
            Produit = produit;
            Client = client;
        }
    }
}