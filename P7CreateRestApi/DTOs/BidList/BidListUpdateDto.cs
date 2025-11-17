using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs.BidList
{
    /// <summary>
    /// DTO utilisé pour mettre à jour une BidList existante.
    /// Contient uniquement les champs modifiables par le client. (PUT)
    /// </summary>
    public class BidListUpdateDto
    {
        [StringLength(50, ErrorMessage = "Le compte ne peut pas dépasser 50 caractères")]
        public string Account { get; set; }

        public string BidType { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La quantité d'achat doit être un nombre positif")]
        public double? BidQuantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La quantité de vente doit être un nombre positif")]
        public double? AskQuantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Le prix d'achat doit être un nombre positif")]
        public double? Bid { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "e prix de vente doit être un nombre positif")]
        public double? Ask { get; set; }

        public string Benchmark { get; set; }
        public string Commentary { get; set; }
        public string BidSecurity { get; set; }
        public string BidStatus { get; set; }
        public string Trader { get; set; }
        public string Book { get; set; }
        public string DealName { get; set; }
        public string DealType { get; set; }
        public string SourceListId { get; set; }
        public string Side { get; set; }

        [Required(ErrorMessage = "Le nom de révision est obligatoire")]
        [StringLength(50, ErrorMessage = "Le nom de révision ne peut pas dépasser 50 caractères")]
        public string RevisionName { get; set; }
    }
}
