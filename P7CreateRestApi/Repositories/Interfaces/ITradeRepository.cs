using P7CreateRestApi.Domain;

public interface ITradeRepository
{
    Task<IEnumerable<Trade>> GetAllAsync();
    Task<Trade> GetByIdAsync(int id);
    Task<Trade> CreateAsync(Trade trade);
    Task UpdateAsync(Trade trade);
    Task DeleteAsync(int id);
}