﻿namespace P7CreateRestApi.DTOs.Bids
{
    /// <summary>
    /// DTO utilisé pour mettre à jour une BidList existante.
    /// Contient uniquement les champs modifiables par le client. (PUT)
    /// </summary>
    public class BidListUpdateDto
    {
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
