using P7CreateRestApi.DTOs.RuleName;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface IRuleNameService
    {
        Task<List<RuleNameReadDto>> GetAllAsync();
        Task<RuleNameReadDto?> GetByIdAsync(int id);
        Task<RuleNameReadDto> CreateAsync(RuleNameCreateDto dto);
        Task<RuleNameReadDto?> UpdateAsync(int id, RuleNameUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
