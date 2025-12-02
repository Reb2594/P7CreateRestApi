using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface IBidRepository
    {
        Task<List<Bid>> GetAllAsync();
        Task<Bid> GetByIdAsync(int id);
        Task<Bid> CreateAsync(Bid bid);
        Task<Bid> UpdateAsync(Bid bid);
        Task DeleteAsync(int id);
    }
}
