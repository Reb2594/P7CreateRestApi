using P7CreateRestApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Data;

namespace P7CreateRestApi.Repositories
{
    public class CurvePointRepository : ICurvePointRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger<CurvePointRepository> _logger;

        public CurvePointRepository(LocalDbContext dbContext, ILogger<CurvePointRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<CurvePoint>> GetAllAsync()
        {
            return await _dbContext.CurvePoints.ToListAsync();
        }

        public async Task<CurvePoint?> GetByIdAsync(int id)
        {
            return await _dbContext.CurvePoints.FindAsync(id);
        }

        public async Task<CurvePoint> CreateAsync(CurvePoint curvePoint)
        {
            try
            {
                await _dbContext.CurvePoints.AddAsync(curvePoint);
                await _dbContext.SaveChangesAsync();
                return curvePoint;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erreur DB lors de la création du CurvePoint");
                throw;
            }
        }

        public async Task<CurvePoint> UpdateAsync(CurvePoint curvePoint)
        {
            try
            {
                _dbContext.CurvePoints.Update(curvePoint);
                await _dbContext.SaveChangesAsync();
                return curvePoint;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erreur DB lors de la mise à jour du CurvePoint {Id}", curvePoint.Id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var curvePoint = await _dbContext.CurvePoints.FindAsync(id);
            if (curvePoint != null)
            {
                _dbContext.CurvePoints.Remove(curvePoint);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}