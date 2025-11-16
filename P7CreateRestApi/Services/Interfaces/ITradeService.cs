using P7CreateRestApi.DTOs.Trade;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface ITradeService
    {
        Task<List<TradeReadDto>> GetAllAsync();
        Task<TradeReadDto?> GetByIdAsync(int id);
        Task<TradeReadDto> CreateAsync(TradeCreateDto dto);
        Task<TradeReadDto?> UpdateAsync(int id, TradeUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
