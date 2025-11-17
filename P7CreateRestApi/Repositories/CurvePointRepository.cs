using P7CreateRestApi.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Repositories.Interfaces;

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
            try
            {
                var curvePoints = await _dbContext.CurvePoints.ToListAsync();
                if (curvePoints.Count < 1)
                {
                    _logger.LogWarning("Aucun CurvePoint trouvé");
                }
                return curvePoints;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la recherche des CurvePoints");
                throw;
            }
        }
        public async Task<CurvePoint?> GetByIdAsync(int id)
        {
            try
            {
                var curvePoint = await _dbContext.CurvePoints.FindAsync(id);
                if (curvePoint == null)
                {
                    _logger.LogWarning("CurvePoint avec l'ID {Id} non trouvé", id);
                }
                return curvePoint;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la recherche du CurvePoint {Id}", id);
                throw;
            }
        }

        public async Task<CurvePoint> CreateAsync(CurvePoint curvePoint)
        {
            try
            {
                await _dbContext.CurvePoints.AddAsync(curvePoint);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("CurvePoint créé avec succès, ID: {Id}", curvePoint.Id);
                return curvePoint;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création du CurvePoint");
                throw;
            }
        }

        public async Task<CurvePoint?> UpdateAsync(CurvePoint curvePoint)
        {
            try
            {
                _dbContext.CurvePoints.Update(curvePoint);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("CurvePoint avec l'ID {Id} mis à jour avec succès", curvePoint.Id);
                return curvePoint;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la mise à jour du CurvePoint {Id}", curvePoint.Id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var curvePoint = await _dbContext.CurvePoints.FindAsync(id);
                if (curvePoint != null)
                {
                    _dbContext.CurvePoints.Remove(curvePoint);
                    await _dbContext.SaveChangesAsync();
                    _logger.LogInformation("CurvePoint avec l'ID {Id} supprimé avec succès", id);
                }
                else
                {
                    _logger.LogWarning("CurvePoint avec l'ID {Id} non trouvé pour la suppression", id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppression du CurvePoint {Id}", id);
                throw;
            }

        }
    }
}
