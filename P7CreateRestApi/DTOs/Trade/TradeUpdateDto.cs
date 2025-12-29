using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs.Trade
{
    /// <summary>
    /// DTO utilisé pour mettre à jour une trade existante.
    /// Contient uniquement les champs modifiables par le client. (PUT)
    /// </summary>
    public class TradeUpdateDto
    {
        [StringLength(50, ErrorMessage = "Le compte ne peut pas dépasser 50 caractères")]
        public string Account { get; set; }

        public string AccountType { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La quantité d'achat doit être un nombre positif")]
        public double? BuyQuantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La quantité de vente doit être un nombre positif")]
        public double? SellQuantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Le prix d'achat doit être un nombre positif")]
        public double? BuyPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Le prix de vente doit être un nombre positif")]
        public double? SellPrice { get; set; }

        public string TradeSecurity { get; set; }
        public string TradeStatus { get; set; }
        public string Trader { get; set; }
        public string Benchmark { get; set; }
        public string Book { get; set; }

        [Required(ErrorMessage = "Le nom de révision est obligatoire")]
        [StringLength(50, ErrorMessage = "Le nom de révision ne peut pas dépasser 50 caractères")]
        public string RevisionName { get; set; }

        public string DealName { get; set; }
        public string DealType { get; set; }
        public string SourceListId { get; set; }
        public string Side { get; set; }
    }
}