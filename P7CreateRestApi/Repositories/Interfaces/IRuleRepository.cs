using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface IRuleRepository
    {
        Task<List<Rule>> GetAllAsync();
        Task<Rule> GetByIdAsync(int id);
        Task<Rule> CreateAsync(Rule rule);
        Task<Rule> UpdateAsync(Rule rule);
        Task DeleteAsync(int id);
    }
}
