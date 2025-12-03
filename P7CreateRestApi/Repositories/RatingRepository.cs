using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;

namespace P7CreateRestApi.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger<RatingRepository> _logger;

        public RatingRepository(LocalDbContext dbContext, ILogger<RatingRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<Rating>> GetAllAsync()
        {
            return await _dbContext.Ratings.ToListAsync();
        }

        public async Task<Rating?> GetByIdAsync(int id)
        {
            return await _dbContext.Ratings.FindAsync(id);
        }

        public async Task<Rating> CreateAsync(Rating rating)
        {
            try
            {
                await _dbContext.Ratings.AddAsync(rating);
                await _dbContext.SaveChangesAsync();
                return rating;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erreur DB lors de la création du rating");
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
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erreur DB lors de la mise à jour du rating {Id}", rating.Id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var rating = await _dbContext.Ratings.FindAsync(id);
            if (rating != null)
            {
                _dbContext.Ratings.Remove(rating);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}