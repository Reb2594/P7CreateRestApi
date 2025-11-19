using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface IRuleNameRepository
    {
        Task<List<RuleName>> GetAllAsync();
        Task<RuleName> GetByIdAsync(int id);
        Task<RuleName> CreateAsync(RuleName rule);
        Task<RuleName> UpdateAsync(RuleName rule);
        Task DeleteAsync(int id);
    }
}
