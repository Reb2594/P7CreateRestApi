using P7CreateRestApi.DTOs.CurvePoint;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface ICurvePointService
    {
        Task<List<CurvePointReadDto>> GetAllAsync();
        Task<CurvePointReadDto?> GetByIdAsync(int id);
        Task<CurvePointReadDto> CreateAsync(CurvePointCreateDto dto);
        Task<CurvePointReadDto?> UpdateAsync(int id, CurvePointUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
