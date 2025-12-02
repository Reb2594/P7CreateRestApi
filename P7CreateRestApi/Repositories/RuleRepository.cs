using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;

namespace P7CreateRestApi.Repositories
{
    public class RuleRepository : IRuleRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger _logger;
        public RuleRepository(LocalDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<Rule>> GetAllAsync()
        {
            try
            {
                var rules = await _dbContext.Rules.ToListAsync();
                if (rules.Count < 1)
                {
                    _logger.LogWarning("Aucun Rule trouvé");
                }
                return rules;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la recherche des Rules");
                throw;
            }

        }

        public async Task<Rule?> GetByIdAsync(int id)
        {
            try
            {
                var rule = await _dbContext.Rules.FindAsync(id);
                if (rule == null)
                {
                    _logger.LogWarning("Rule avec l'ID {Id} non trouvé", id);
                }
                return rule;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la recherche du Rule {Id}", id);
                throw;
            }
        }

        public async Task<Rule> CreateAsync(Rule rule)
        {
            try
            {
                await _dbContext.Rules.AddAsync(rule);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Rule créé avec succès, ID: {Id}", rule.Id);
                return rule;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création du Rule");
                throw;
            }
        }

        public async Task<Rule> UpdateAsync(Rule rule)
        {
            try
            {
                _dbContext.Rules.Update(rule);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Rule mis à jour avec succès, ID: {Id}", rule.Id);
                return rule;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la mise à jour du Rule avec l'ID {Id}", rule.Id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var rule = await _dbContext.Rules.FindAsync(id);
                if (rule != null)
                {
                    _dbContext.Rules.Remove(rule);
                    await _dbContext.SaveChangesAsync();
                    _logger.LogInformation("Rule supprimé avec succès, ID: {Id}", id);
                }
                else
                {
                    _logger.LogWarning("Rule avec l'ID {Id} non trouvé pour suppression", id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppression du Rule avec l'ID {Id}", id);
                throw;
            }
        }
    }
}
