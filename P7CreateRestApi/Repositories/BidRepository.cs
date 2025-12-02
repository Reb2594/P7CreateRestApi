using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using P7CreateRestApi.Data;

namespace P7CreateRestApi.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger _logger;

        public BidRepository(LocalDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<Bid>> GetAllAsync()
        {
            try
            {
                var bids = await _dbContext.Bids.ToListAsync();
                if (bids.Count < 1)
                {
                    _logger.LogWarning("Aucun Bid trouvé");
                }
                return bids;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la recherche des bids");
                throw;
            }
        }
        public async Task<Bid?> GetByIdAsync(int id)
        {
            try
            {
                var bid = await _dbContext.Bids.FindAsync(id);
                if (bid == null)
                {
                    _logger.LogWarning("Bid avec l'ID {Id} non trouvé", id);
                }
                return bid;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la recherche du bid {Id}", id);
                throw;
            }
        }

        public async Task<Bid> CreateAsync(Bid bid)
        {
            try
            {
                await _dbContext.Bids.AddAsync(bid);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Bid créé avec succès, ID: {Id}", bid.BidId);
                return bid;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création du bid");
                throw;
            }                
        }

        public async Task<Bid> UpdateAsync(Bid bid)
        {
            try
            {
                _dbContext.Bids.Update(bid);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Bid mis à jour avec succès, ID: {Id}", bid.BidId);
                return bid;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la mise à jour du bid {Id}", bid.BidId);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var bid = await _dbContext.Bids.FindAsync(id);
                if (bid != null)
                {
                    _dbContext.Bids.Remove(bid);
                    await _dbContext.SaveChangesAsync();
                    _logger.LogInformation("Bid supprimé avec succès, ID: {Id}", id);
                }
                else
                {
                    _logger.LogWarning("Tentative de suppression d'un bid non trouvé, ID: {Id}", id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppression du bid {Id}", id);
                throw;
            }
        }
    }
}
