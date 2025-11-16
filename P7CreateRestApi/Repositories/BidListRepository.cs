using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace P7CreateRestApi.Repositories
{
    public class BidListRepository : IBidListRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger _logger;

        public BidListRepository(LocalDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<BidList>> GetAllAsync()
        {
            try
            {
                var bidlists = await _dbContext.BidLists.ToListAsync();
                if (bidlists.Count < 1)
                {
                    _logger.LogWarning("Aucun Bid trouvé");
                }
                return bidlists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la recherche des bids");
                throw;
            }
        }
        public async Task<BidList?> GetByIdAsync(int id)
        {
            try
            {
                var bidlist = await _dbContext.BidLists.FindAsync(id);
                if (bidlist == null)
                {
                    _logger.LogWarning("Bid avec l'ID {Id} non trouvé", id);
                }
                return bidlist;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la recherche du bid {Id}", id);
                throw;
            }
        }

        public async Task<BidList> CreateAsync(BidList bidList)
        {
            try
            {
                await _dbContext.BidLists.AddAsync(bidList);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Bid créé avec succès, ID: {Id}", bidList.BidListId);
                return bidList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création du bid");
                throw;
            }                
        }

        public async Task<BidList> UpdateAsync(BidList bidList)
        {
            try
            {
                _dbContext.BidLists.Update(bidList);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Bid mis à jour avec succès, ID: {Id}", bidList.BidListId);
                return bidList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la mise à jour du bid {Id}", bidList.BidListId);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var bidList = await _dbContext.BidLists.FindAsync(id);
                if (bidList != null)
                {
                    _dbContext.BidLists.Remove(bidList);
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
