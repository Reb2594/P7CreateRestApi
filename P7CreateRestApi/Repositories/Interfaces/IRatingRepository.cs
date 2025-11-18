using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories.Interfaces
{
    public interface IRatingRepository
    {
        Task<List<Rating>> GetAllAsync();
        Task<Rating> GetByIdAsync(int id);
        Task<Rating> CreateAsync (Rating rating);
        Task<Rating> UpdateAsync (Rating rating);
        Task DeleteAsync (int id);
    }
}
