using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services.Interfaces;
using AutoMapper;
using P7CreateRestApi.DTOs.BidList;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Services
{
    public class BidListService : IBidListService
    {
        private readonly IBidListRepository _bidListRepository;
        private readonly IMapper _mapper;

        public BidListService(IBidListRepository bidListRepository, IMapper mapper)
        {
            _bidListRepository = bidListRepository;
            _mapper = mapper;
        }

        public async Task<List<BidListReadDto>> GetAllAsync()
        {
            var bidLists = await _bidListRepository.GetAllAsync();
            return _mapper.Map<List<BidListReadDto>>(bidLists);
        }

        public async Task<BidListReadDto?> GetByIdAsync(int id)
        {
            var bidList = await _bidListRepository.GetByIdAsync(id);
            return bidList == null ? null : _mapper.Map<BidListReadDto>(bidList);
        }

        public async Task<BidListReadDto> CreateAsync(BidListCreateDto dto)
        {
            var bidList = _mapper.Map<BidList>(dto);
            var createdBidList = await _bidListRepository.CreateAsync(bidList);
            return _mapper.Map<BidListReadDto>(createdBidList);
        }

        public async Task<BidListReadDto?> UpdateAsync(int id, BidListUpdateDto dto)
        {
            var existingBidList = await _bidListRepository.GetByIdAsync(id);
            if (existingBidList == null)
                return null;

            _mapper.Map(dto, existingBidList);
            await _bidListRepository.UpdateAsync(existingBidList);
            return _mapper.Map<BidListReadDto>(existingBidList);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingBidList = await _bidListRepository.GetByIdAsync(id);
            if (existingBidList == null)
                return false;
            await _bidListRepository.DeleteAsync(id);
            return true;
        }
    }
}
