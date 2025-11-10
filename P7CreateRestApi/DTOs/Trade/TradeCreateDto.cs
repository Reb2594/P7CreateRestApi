using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs.Trade
{
    public class TradeCreateDto
    {
        [Required(ErrorMessage = "Le compte est obligatoire")]
        [StringLength(50, ErrorMessage = "Le compte ne peut pas dépasser 50 caractères")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Le type de compte est obligatoire")]
        public string AccountType { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La quantité d'achat doit être un nombre positif")]
        public double? BuyQuantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La quantité de vente doit être un nombre positif")]
        public double? SellQuantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Le prix d'achat doit être un nombre positif")]
        public double? BuyPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Le prix de vente doit être un nombre positif")]
        public double? SellPrice { get; set; }

        [Required(ErrorMessage = "La sécurité de trading est obligatoire")]
        public string TradeSecurity { get; set; }
    }
}