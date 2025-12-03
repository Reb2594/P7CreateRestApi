using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Data
{
    public class LocalDbContext : IdentityDbContext<ApplicationUser>
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }

        public DbSet<Bid> Bids { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<CurvePoint> CurvePoints { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Rating>().HasData(
                new Rating { Id = 1, MoodysRating = "AAA", SandPRating = "AAA", FitchRating = "AAA", OrderNumber = 1 },
                new Rating { Id = 2, MoodysRating = "AA", SandPRating = "AA", FitchRating = "AA", OrderNumber = 2 },
                new Rating { Id = 3, MoodysRating = "A", SandPRating = "A", FitchRating = "A", OrderNumber = 3 }
            );

            builder.Entity<Rule>().HasData(
                new Rule { Id = 1, Name = "Rule1", Description = "Vérifie la quantité", Json = "{}", Template = "T1", SqlStr = "", SqlPart = "" },
                new Rule { Id = 2, Name = "Rule2", Description = "Vérifie le prix", Json = "{}", Template = "T2", SqlStr = "", SqlPart = "" }
            );

            builder.Entity<Bid>().HasData(
                new Bid
                {
                    BidId = 1,
                    Account = "Account1",
                    BidType = "Type1",
                    BidQuantity = 1000,
                    AskQuantity = 1100,
                    BidPrice = 99.5,
                    AskPrice = 100.5,
                    Benchmark = "Bench1",
                    BidDate = DateTime.UtcNow.AddDays(-1),
                    Commentary = "Test bid 1",
                    BidSecurity = "Sec1",
                    BidStatus = "OPEN",
                    Trader = "Trader1",
                    Book = "Book1",
                    CreationName = "Admin",
                    RevisionName = "Admin",
                    DealName = "Deal1",
                    DealType = "TypeA",
                    SourceListId = "Source1",
                    Side = "Buy"
                },
                new Bid
                {
                    BidId = 2,
                    Account = "Account2",
                    BidType = "Type2",
                    BidQuantity = 2000,
                    AskQuantity = 2100,
                    BidPrice = 101.5,
                    AskPrice = 102.5,
                    Benchmark = "Bench2",
                    BidDate = DateTime.UtcNow.AddDays(-2),
                    Commentary = "Test bid 2",
                    BidSecurity = "Sec2",
                    BidStatus = "CLOSED",
                    Trader = "Trader2",
                    Book = "Book2",
                    CreationName = "Admin",
                    RevisionName = "Admin",
                    DealName = "Deal2",
                    DealType = "TypeB",
                    SourceListId = "Source2",
                    Side = "Sell"
                }
            );

            builder.Entity<Trade>().HasData(
                new Trade
                {
                    TradeId = 1,
                    Account = "Account1",
                    AccountType = "TypeA",
                    BuyQuantity = 100,
                    SellQuantity = 50,
                    BuyPrice = 99.5,
                    SellPrice = 100.0,
                    TradeDate = DateTime.UtcNow.AddDays(-1),
                    TradeSecurity = "Sec1",
                    TradeStatus = "OPEN",
                    Trader = "Trader1",
                    Benchmark = "Bench1",
                    Book = "Book1",
                    CreationName = "Admin",
                    RevisionName = "Admin",
                    DealName = "Deal1",
                    DealType = "TypeA",
                    SourceListId = "Source1",
                    Side = "Buy"
                }
            );

            builder.Entity<CurvePoint>().HasData(
                new CurvePoint { Id = 1, CurveId = 1, AsOfDate = DateTime.UtcNow, Term = 1.0, CurvePointValue = 0.01 },
                new CurvePoint { Id = 2, CurveId = 1, AsOfDate = DateTime.UtcNow, Term = 2.0, CurvePointValue = 0.015 }
            );
        }
    }
}
