using P7CreateRestApi.DTOs.Rule;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface IRuleService
    {
        Task<List<RuleReadDto>> GetAllAsync();
        Task<RuleReadDto?> GetByIdAsync(int id);
        Task<RuleReadDto> CreateAsync(RuleCreateDto dto);
        Task<RuleReadDto?> UpdateAsync(int id, RuleUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
