using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Domain;
using Microsoft.Extensions.Logging;
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
            try
            {
                var trades = await _dbContext.Trades.ToListAsync();
                if (trades.Count < 1)
                {
                    _logger.LogWarning("Aucune Trade trouvée");
                }
                return trades ;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la recherche des trades");
                throw;
            }
        }
        public async Task<Trade> GetByIdAsync(int id)
        {
            try
            {
                var trade = await _dbContext.Trades.FindAsync(id);
                if (trade == null)
                {
                    _logger.LogWarning("Trade avec l'ID {Id} non trouvé", id);
                }
                return trade;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la recherche du trade {Id}", id);
                throw;
            }
        }
        public async Task<Trade> CreateAsync(Trade trade)
        {
            try
            {
                await _dbContext.Trades.AddAsync(trade);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Trade créé avec succès, ID: {Id}", trade.TradeId);
                return trade;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création du trade");
                throw;
            }
        }
        public async Task<Trade> UpdateAsync(Trade trade)
        {
            try
            {
                _dbContext.Trades.Update(trade);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Trade mis à jour avec succès, ID: {Id}", trade.TradeId);
                return trade;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la mise à jour du trade {Id}", trade.TradeId);
                throw;
            }
        }
        public async Task DeleteAsync(int id)
        {
            try
            {
                var trade = await _dbContext.Trades.FindAsync(id);
                if (trade != null)
                {
                    _dbContext.Trades.Remove(trade);
                    await _dbContext.SaveChangesAsync();
                    _logger.LogInformation("Trade supprimé avec succès, ID: {Id}", id);
                }
                else
                {
                    _logger.LogWarning("Tentative de suppression d'un trade inexistant, ID: {Id}", id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppression du trade {Id}", id);
                throw;
            }
        }

    }
}
