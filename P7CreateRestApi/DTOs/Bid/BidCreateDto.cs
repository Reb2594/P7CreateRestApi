using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs.Bid
{
    /// <summary>
    /// DTO utilisé pour créer un nouveau bid.
    /// Contient uniquement les champs à renseigner par le client. (POST)
    /// </summary>
    public class BidCreateDto
    {
        [Required(ErrorMessage = "Le champ 'Compte' est obligatoire.")]
        [StringLength(50, ErrorMessage = "Le champ 'Compte' ne peut pas dépasser 50 caractères.")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Le type d'offre est obligatoire.")]
        [StringLength(30, ErrorMessage = "Le type d'offre ne peut pas dépasser 30 caractères.")]
        public string BidType { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "La quantité d'achat doit être un nombre positif.")]
        public double? BidQuantity { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "La quantité de vente doit être un nombre positif.")]
        public double? AskQuantity { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Le prix d'achat doit être un nombre positif.")]
        public double? Bid { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Le prix de vente doit être un nombre positif.")]
        public double? Ask { get; set; }

        [StringLength(50)]
        public string Benchmark { get; set; }

        [StringLength(255)]
        public string Commentary { get; set; }

        [StringLength(50)]
        public string BidSecurity { get; set; }

        [StringLength(20)]
        public string BidStatus { get; set; }

        [StringLength(50)]
        public string Trader { get; set; }

        [StringLength(50)]
        public string Book { get; set; }

        [StringLength(50)]
        public string DealName { get; set; }

        [StringLength(50)]
        public string DealType { get; set; }

        [StringLength(50)]
        public string SourceListId { get; set; }

        [StringLength(10)]
        public string Side { get; set; }
    }
}
