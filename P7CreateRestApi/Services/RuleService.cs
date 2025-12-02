using P7CreateRestApi.Domain;
using P7CreateRestApi.Services.Interfaces;
using P7CreateRestApi.DTOs.Rule;
using AutoMapper;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Services
{
    public class RuleService : IRuleService
    {
        private readonly IRuleRepository _ruleRepository;
        private readonly IMapper _mapper;
        public RuleService(IRuleRepository ruleRepository, IMapper mapper)
        {
            _ruleRepository = ruleRepository;
            _mapper = mapper;
        }

        public async Task<List<RuleReadDto>> GetAllAsync()
        {
            var rules = await _ruleRepository.GetAllAsync();
            return _mapper.Map<List<RuleReadDto>>(rules);
        }
        
        public async Task<RuleReadDto?> GetByIdAsync(int id)
        {
            var rule = await _ruleRepository.GetByIdAsync(id);
            return rule == null ? null :_mapper.Map<RuleReadDto>(rule);
        }

        public async Task<RuleReadDto> CreateAsync(RuleCreateDto dto)
        {
            var rule = _mapper.Map<Rule>(dto);
            var createdRule = await _ruleRepository.CreateAsync(rule);
            return _mapper.Map<RuleReadDto>(createdRule);
        }

        public async Task<RuleReadDto?> UpdateAsync(int id, RuleUpdateDto dto)
        {
            var existingRule = await _ruleRepository.GetByIdAsync(id);
            if (existingRule == null)
                return null;
            _mapper.Map(dto, existingRule);
            await _ruleRepository.UpdateAsync(existingRule);
            return _mapper.Map<RuleReadDto>(existingRule);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingRule = await _ruleRepository.GetByIdAsync(id);
            if (existingRule == null)
                return false;
            await _ruleRepository.DeleteAsync(id);
            return true;
        }
    }
}
