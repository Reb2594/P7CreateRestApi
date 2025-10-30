namespace P7CreateRestApi.DTOs.Bids
{    
    /// <summary>
    /// DTO utilisé pour créer une nouvelle BidList.
    /// Représente les données que le client envoie à l'API lors d'une création (POST).
    /// </summary>
    public class BidListCreateDto
    {
        // Champs que le client doit fournir pour créer un Bid.
        public string Account { get; set; }
        public string BidType { get; set; }
        public double? BidQuantity { get; set; }
        public double? AskQuantity { get; set; }
        public double? Bid { get; set; }
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
    }
}

