namespace P7CreateRestApi.Domain
{
    public class Trade
    {
        public int TradeId { get; private set; }
        public string Account { get; set; }
        public string AccountType { get; set; }
        public double? BuyQuantity { get; set; }
        public double? SellQuantity { get; set; }
        public double? BuyPrice { get; set; }
        public double? SellPrice { get; set; }
        public DateTime? TradeDate { get; private set; } = DateTime.UtcNow;
        public string TradeSecurity { get; set; }
        public string TradeStatus { get; set; }
        public string Trader { get; set; }
        public string Benchmark { get; set; }
        public string Book { get; set; }
        public string CreationName { get; set; }
        public DateTime? CreationDate { get; set; }
        public string RevisionName { get; set; }
        public DateTime? RevisionDate { get; private set; }
        public string DealName { get; set; }
        public string DealType { get; set; }
        public string SourceListId { get; set; }
        public string Side { get; set; }

        public void SetRevision(string revisionName)
        {
            if (string.IsNullOrEmpty(revisionName))
                throw new ArgumentException("Le nom de révision ne peut pas être vide", nameof(revisionName));

            RevisionName = revisionName;
            RevisionDate = DateTime.UtcNow;
        }
    }
}