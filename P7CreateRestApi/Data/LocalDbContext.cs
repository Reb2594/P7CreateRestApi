using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace P7CreateRestApi.Data
{
    public class LocalDbContext : IdentityDbContext<ApplicationUser>
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Bid> Bids { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<CurvePoint> CurvePoints { get; set; }
    }
}