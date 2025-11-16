using P7CreateRestApi.Domain;

public interface ITradeRepository
{
    Task<List<Trade>> GetAllAsync();
    Task<Trade> GetByIdAsync(int id);
    Task<Trade> CreateAsync(Trade trade);
    Task<Trade> UpdateAsync(Trade trade);
    Task DeleteAsync(int id);
}