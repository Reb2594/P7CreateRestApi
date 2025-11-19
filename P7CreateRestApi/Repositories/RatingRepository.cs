using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;

namespace P7CreateRestApi.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger _logger;

        public RatingRepository(LocalDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<Rating>> GetAllAsync()
        {
            try
            {
                var ratings = await _dbContext.Ratings.ToListAsync();
                if (ratings.Count < 1)
                {
                    _logger.LogWarning("Aucun Rating trouvé");
                }
                return ratings;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la recherche des ratings");
                throw;
            }

        }
        public async Task<Rating> GetByIdAsync(int id)
        {
            try
            {
                var rating = await _dbContext.Ratings.FindAsync(id);
                if (rating == null)
                {
                    _logger.LogWarning("Rating avec l'ID {Id} non trouvé", id);
                }
                return rating;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la recherche du rating {Id}", id);
                throw;
            }
        }
        public async Task<Rating> CreateAsync(Rating rating)
        {
            try
            {
                await _dbContext.Ratings.AddAsync(rating);
                await _dbContext.SaveChangesAsync();
                return rating;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création du rating");
                throw;
            }
        }
        public async Task<Rating> UpdateAsync(Rating rating)
        {
            try
            {
                _dbContext.Ratings.Update(rating);
                await _dbContext.SaveChangesAsync();
                return rating;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la mise à jour du rating avec l'ID {Id}", rating.Id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var rating = await _dbContext.Ratings.FindAsync(id);
                if (rating == null)
                {
                    _logger.LogWarning("Rating avec l'ID {Id} non trouvé pour suppression", id);
                    return;
                }
                _dbContext.Ratings.Remove(rating);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppression du rating avec l'ID {Id}", id);
                throw;
            }
        }
    }
}
