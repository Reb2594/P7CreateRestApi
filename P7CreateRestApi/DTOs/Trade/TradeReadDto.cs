namespace P7CreateRestApi.DTOs.Trade
{
    public class TradeReadDto
    {
        public int TradeId { get; set; }
        public string Account { get; set; }
        public string AccountType { get; set; }
        public double? BuyQuantity { get; set; }
        public double? SellQuantity { get; set; }
        public double? BuyPrice { get; set; }
        public double? SellPrice { get; set; }
        public DateTime? TradeDate { get; set; }
        public string TradeSecurity { get; set; }
        public string TradeStatus { get; set; }
    }
}