using P7CreateRestApi.Domain;
using P7CreateRestApi.Services.Interfaces;
using P7CreateRestApi.DTOs.RuleName;
using AutoMapper;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Services
{
    public class RuleNameService : IRuleNameService
    {
        private readonly IRuleNameRepository _ruleNameRepository;
        private readonly IMapper _mapper;
        public RuleNameService(IRuleNameRepository ruleNameRepository, IMapper mapper)
        {
            _ruleNameRepository = ruleNameRepository;
            _mapper = mapper;
        }

        public async Task<List<RuleNameReadDto>> GetAllAsync()
        {
            var ruleNames = await _ruleNameRepository.GetAllAsync();
            return _mapper.Map<List<RuleNameReadDto>>(ruleNames);
        }
        
        public async Task<RuleNameReadDto?> GetByIdAsync(int id)
        {
            var ruleName = await _ruleNameRepository.GetByIdAsync(id);
            return ruleName == null ? null :_mapper.Map<RuleNameReadDto>(ruleName);
        }

        public async Task<RuleNameReadDto> CreateAsync(RuleNameCreateDto dto)
        {
            var ruleName = _mapper.Map<RuleName>(dto);
            var createdRuleName = await _ruleNameRepository.CreateAsync(ruleName);
            return _mapper.Map<RuleNameReadDto>(createdRuleName);
        }

        public async Task<RuleNameReadDto?> UpdateAsync(int id, RuleNameUpdateDto dto)
        {
            var existingRuleName = await _ruleNameRepository.GetByIdAsync(id);
            if (existingRuleName == null)
                return null;
            _mapper.Map(dto, existingRuleName);
            await _ruleNameRepository.UpdateAsync(existingRuleName);
            return _mapper.Map<RuleNameReadDto>(existingRuleName);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingRuleName = await _ruleNameRepository.GetByIdAsync(id);
            if (existingRuleName == null)
                return false;
            await _ruleNameRepository.DeleteAsync(id);
            return true;
        }
    }
}
