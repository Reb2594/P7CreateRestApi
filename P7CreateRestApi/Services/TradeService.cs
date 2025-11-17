using P7CreateRestApi.DTOs.Trade;
using P7CreateRestApi.Services.Interfaces;
using P7CreateRestApi.Domain;
using AutoMapper;

namespace P7CreateRestApi.Services
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly IMapper _mapper;

        public TradeService(ITradeRepository tradeRepository, IMapper mapper)
        {
            _tradeRepository = tradeRepository;
            _mapper = mapper;
        }

        public async Task<List<TradeReadDto>> GetAllAsync()
        {
            var trades = await _tradeRepository.GetAllAsync();
            return _mapper.Map<List<TradeReadDto>>(trades);
        }

        public async Task<TradeReadDto?> GetByIdAsync(int id)
        {
            var trade = await _tradeRepository.GetByIdAsync(id);
            return trade == null ? null : _mapper.Map<TradeReadDto>(trade);
        }

        public async Task<TradeReadDto> CreateAsync(TradeCreateDto dto)
        {
            Trade trade = _mapper.Map<Trade>(dto);
            Trade createdTrade = await _tradeRepository.CreateAsync(trade);
            return _mapper.Map<TradeReadDto>(createdTrade);
        }

        public async Task<TradeReadDto?> UpdateAsync(int id, TradeUpdateDto dto)
        {
            Trade existingTrade = await _tradeRepository.GetByIdAsync(id);
            if (existingTrade == null)
                return null;
            _mapper.Map(dto, existingTrade);
            await _tradeRepository.UpdateAsync(existingTrade);
            return _mapper.Map<TradeReadDto>(existingTrade);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Trade existingTrade = await _tradeRepository.GetByIdAsync(id);
            if (existingTrade == null) 
                return false;
            await _tradeRepository.DeleteAsync(id);
            return true;
        }
    }
}
