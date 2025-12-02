using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services.Interfaces;
using AutoMapper;
using P7CreateRestApi.DTOs.Bid;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Services
{
    public class BidService : IBidService
    {
        private readonly IBidRepository _bidRepository;
        private readonly IMapper _mapper;

        public BidService(IBidRepository bidRepository, IMapper mapper)
        {
            _bidRepository = bidRepository;
            _mapper = mapper;
        }

        public async Task<List<BidReadDto>> GetAllAsync()
        {
            var bids = await _bidRepository.GetAllAsync();
            return _mapper.Map<List<BidReadDto>>(bids);
        }

        public async Task<BidReadDto?> GetByIdAsync(int id)
        {
            var bid = await _bidRepository.GetByIdAsync(id);
            return bid == null ? null : _mapper.Map<BidReadDto>(bid);
        }

        public async Task<BidReadDto> CreateAsync(BidCreateDto dto)
        {
            var bid = _mapper.Map<Bid>(dto);
            var createdBid = await _bidRepository.CreateAsync(bid);
            return _mapper.Map<BidReadDto>(createdBid);
        }

        public async Task<BidReadDto?> UpdateAsync(int id, BidUpdateDto dto)
        {
            var existingBid = await _bidRepository.GetByIdAsync(id);
            if (existingBid == null)
                return null;

            _mapper.Map(dto, existingBid);
            await _bidRepository.UpdateAsync(existingBid);
            return _mapper.Map<BidReadDto>(existingBid);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingBid = await _bidRepository.GetByIdAsync(id);
            if (existingBid == null)
                return false;
            await _bidRepository.DeleteAsync(id);
            return true;
        }
    }
}
