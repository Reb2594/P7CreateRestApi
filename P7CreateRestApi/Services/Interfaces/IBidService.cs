using P7CreateRestApi.DTOs.Bid;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface IBidService
    {
        Task<List<BidReadDto>> GetAllAsync();
        Task<BidReadDto?> GetByIdAsync(int id);
        Task<BidReadDto> CreateAsync(BidCreateDto dto);
        Task<BidReadDto?> UpdateAsync(int id, BidUpdateDto dto);
        Task<bool> DeleteAsync(int id);        
    }
}
