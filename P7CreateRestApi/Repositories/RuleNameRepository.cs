using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;

namespace P7CreateRestApi.Repositories
{
    public class RuleNameRepository : IRuleNameRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger _logger;
        public RuleNameRepository(LocalDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<RuleName>> GetAllAsync()
        {
            try
            {
                var ruleNames = await _dbContext.RuleNames.ToListAsync();
                if (ruleNames.Count < 1)
                {
                    _logger.LogWarning("Aucun RuleName trouvé");
                }
                return ruleNames;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la recherche des RuleNames");
                throw;
            }

        }

        public async Task<RuleName?> GetByIdAsync(int id)
        {
            try
            {
                var ruleName = await _dbContext.RuleNames.FindAsync(id);
                if (ruleName == null)
                {
                    _logger.LogWarning("RuleName avec l'ID {Id} non trouvé", id);
                }
                return ruleName;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la recherche du RuleName {Id}", id);
                throw;
            }
        }

        public async Task<RuleName> CreateAsync(RuleName ruleName)
        {
            try
            {
                await _dbContext.RuleNames.AddAsync(ruleName);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("RuleName créé avec succès, ID: {Id}", ruleName.Id);
                return ruleName;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création du RuleName");
                throw;
            }
        }

        public async Task<RuleName> UpdateAsync(RuleName ruleName)
        {
            try
            {
                _dbContext.RuleNames.Update(ruleName);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("RuleName mis à jour avec succès, ID: {Id}", ruleName.Id);
                return ruleName;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la mise à jour du RuleName avec l'ID {Id}", ruleName.Id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var ruleName = await _dbContext.RuleNames.FindAsync(id);
                if (ruleName != null)
                {
                    _dbContext.RuleNames.Remove(ruleName);
                    await _dbContext.SaveChangesAsync();
                    _logger.LogInformation("RuleName supprimé avec succès, ID: {Id}", id);
                }
                else
                {
                    _logger.LogWarning("RuleName avec l'ID {Id} non trouvé pour suppression", id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppression du RuleName avec l'ID {Id}", id);
                throw;
            }
        }
    }
}
