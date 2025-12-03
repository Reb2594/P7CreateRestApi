using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;

namespace P7CreateRestApi.Repositories
{
    public class RuleRepository : IRuleRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger<RuleRepository> _logger;

        public RuleRepository(LocalDbContext dbContext, ILogger<RuleRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<Rule>> GetAllAsync()
        {
            return await _dbContext.Rules.ToListAsync();
        }

        public async Task<Rule?> GetByIdAsync(int id)
        {
            return await _dbContext.Rules.FindAsync(id);
        }

        public async Task<Rule> CreateAsync(Rule rule)
        {
            try
            {
                await _dbContext.Rules.AddAsync(rule);
                await _dbContext.SaveChangesAsync();
                return rule;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erreur DB lors de la création de la règle");
                throw;
            }
        }

        public async Task<Rule> UpdateAsync(Rule rule)
        {
            try
            {
                _dbContext.Rules.Update(rule);
                await _dbContext.SaveChangesAsync();
                return rule;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erreur DB lors de la mise à jour de la règle {Id}", rule.Id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var rule = await _dbContext.Rules.FindAsync(id);
            if (rule != null)
            {
                _dbContext.Rules.Remove(rule);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}