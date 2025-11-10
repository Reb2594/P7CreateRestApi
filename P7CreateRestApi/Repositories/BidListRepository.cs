using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Repositories
{
    public class BidListRepository : IBidListRepository
    {
        private readonly LocalDbContext _dbContext;
        public BidListRepository(LocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BidList>> GetAllAsync()
        {
            return await _dbContext.BidLists.ToListAsync();
        }

        public async Task<BidList?> GetByIdAsync(int id)
        {
            return await _dbContext.BidLists.FindAsync(id);
        }

        public async Task AddAsync(BidList bidList)
        {
            await _dbContext.BidLists.AddAsync(bidList);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(BidList bidList)
        {
            _dbContext.BidLists.Update(bidList);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bidList = await _dbContext.BidLists.FindAsync(id);
            if (bidList != null)
            {
                _dbContext.BidLists.Remove(bidList);
                await _dbContext.SaveChangesAsync();
            }
        }

    }

}
