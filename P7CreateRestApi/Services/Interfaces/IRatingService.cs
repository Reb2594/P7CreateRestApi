using P7CreateRestApi.DTOs.Rating;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface IRatingService
    {
        Task<List<RatingReadDto>> GetAllAsync();
        Task<RatingReadDto?> GetByIdAsync(int id);
        Task<RatingReadDto> CreateAsync(RatingCreateDto dto);
        Task<RatingReadDto?> UpdateAsync(int id, RatingUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
