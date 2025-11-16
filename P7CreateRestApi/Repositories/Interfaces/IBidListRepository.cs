using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface IBidListRepository
    {
        Task<List<BidList>> GetAllAsync();
        Task<BidList> GetByIdAsync(int id);
        Task<BidList> CreateAsync(BidList bidList);
        Task<BidList> UpdateAsync(BidList bidList);
        Task DeleteAsync(int id);
    }
}
