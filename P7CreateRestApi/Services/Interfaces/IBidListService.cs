using P7CreateRestApi.DTOs.BidList;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface IBidListService
    {
        Task<IEnumerable<BidListReadDto>> GetAllAsync();
        Task<BidListReadDto?> GetByIdAsync(int id);
        Task<BidListReadDto> CreateAsync(BidListCreateDto dto);
        Task<BidListReadDto?> UpdateAsync(int id, BidListUpdateDto dto);
        Task<bool> DeleteAsync(int id);        
    }
}
