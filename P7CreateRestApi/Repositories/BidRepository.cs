using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Data;

namespace P7CreateRestApi.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger<BidRepository> _logger;

        public BidRepository(LocalDbContext dbContext, ILogger<BidRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<Bid>> GetAllAsync()
        {
            return await _dbContext.Bids.ToListAsync();
        }

        public async Task<Bid?> GetByIdAsync(int id)
        {
            return await _dbContext.Bids.FindAsync(id);
        }

        public async Task<Bid> CreateAsync(Bid bid)
        {
            try
            {
                await _dbContext.Bids.AddAsync(bid);
                await _dbContext.SaveChangesAsync();
                return bid;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erreur DB lors de la création de l'offre");
                throw;
            }
        }

        public async Task<Bid> UpdateAsync(Bid bid)
        {
            try
            {
                _dbContext.Bids.Update(bid);
                await _dbContext.SaveChangesAsync();
                return bid;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erreur DB lors de la mise à jour de l'offre {BidId}", bid.BidId);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var bid = await _dbContext.Bids.FindAsync(id);
            if (bid != null)
            {
                _dbContext.Bids.Remove(bid);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}