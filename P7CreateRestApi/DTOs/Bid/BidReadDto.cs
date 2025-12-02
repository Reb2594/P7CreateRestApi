namespace P7CreateRestApi.DTOs.Bid
{
    /// <summary>
    /// DTO utilisé pour renvoyer les données d'une Bid au client.
    /// Contient toutes les informations utiles pour l'affichage. (GET)
    /// </summary>
    public class BidReadDto
    {
        public int BidId { get; set; }
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
        public DateTime? CreationDate { get; set; }
        public DateTime? RevisionDate { get; set; }
    }
}
