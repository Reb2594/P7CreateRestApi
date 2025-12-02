namespace P7CreateRestApi.Domain
{
    public class Bid
    {
        public int BidId { get; private set; }

        public string Account { get; set; }

        public string BidType { get; set; }

        public double? BidQuantity { get; set; }

        public double? AskQuantity { get; set; }

        public double? BidPrice { get; set; }

        public double? AskPrice { get; set; }

        public string Benchmark { get; set; }

        public DateTime? BidDate { get; set; }

        public string Commentary { get; set; }

        public string BidSecurity { get; set; }

        public string BidStatus { get; set; }

        public string Trader { get; set; }

        public string Book { get; set; }

        public string CreationName { get; set; }

        public DateTime? CreationDate { get; private set; } = DateTime.UtcNow;

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