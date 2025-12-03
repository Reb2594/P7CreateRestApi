using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;

namespace P7CreateRestApi.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger<TradeRepository> _logger;

        public TradeRepository(LocalDbContext dbContext, ILogger<TradeRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<Trade>> GetAllAsync()
        {
            return await _dbContext.Trades.ToListAsync();
        }

        public async Task<Trade?> GetByIdAsync(int id)
        {
            return await _dbContext.Trades.FindAsync(id);
        }

        public async Task<Trade> CreateAsync(Trade trade)
        {
            try
            {
                await _dbContext.Trades.AddAsync(trade);
                await _dbContext.SaveChangesAsync();
                return trade;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erreur DB lors de la création du trade");
                throw;
            }
        }

        public async Task<Trade> UpdateAsync(Trade trade)
        {
            try
            {
                _dbContext.Trades.Update(trade);
                await _dbContext.SaveChangesAsync();
                return trade;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erreur DB lors de la mise à jour du trade {TradeId}", trade.TradeId);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var trade = await _dbContext.Trades.FindAsync(id);
            if (trade != null)
            {
                _dbContext.Trades.Remove(trade);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}