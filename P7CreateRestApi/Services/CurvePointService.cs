using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs.CurvePoint;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services.Interfaces;
using AutoMapper;

namespace P7CreateRestApi.Services
{
    public class CurvePointService : ICurvePointService
    {
        private readonly ICurvePointRepository _curvePointRepository;
        private readonly IMapper _mapper;
        public CurvePointService(ICurvePointRepository curvePointRepository, IMapper mapper)
        {
            _curvePointRepository = curvePointRepository;
            _mapper = mapper;
        }
        public async Task<List<CurvePointReadDto>> GetAllAsync()
        {
            var curvePoints = await _curvePointRepository.GetAllAsync();
            return _mapper.Map<List<CurvePointReadDto>>(curvePoints);
        }
        public async Task<CurvePointReadDto?> GetByIdAsync(int id)
        {
            var curvePoint = await _curvePointRepository.GetByIdAsync(id);
            return curvePoint == null ? null : _mapper.Map<CurvePointReadDto>(curvePoint);
        }
        public async Task<CurvePointReadDto> CreateAsync(CurvePointCreateDto curvePointCreateDto)
        {
            var curvePoint = _mapper.Map<CurvePoint>(curvePointCreateDto);
            var createdCurvePoint = await _curvePointRepository.CreateAsync(curvePoint);
            return _mapper.Map<CurvePointReadDto>(createdCurvePoint);
        }
        public async Task<CurvePointReadDto?> UpdateAsync(int id, CurvePointUpdateDto curvePointUpdateDto)
        {
            var existingCurvePoint = await _curvePointRepository.GetByIdAsync(id);
            if (existingCurvePoint == null)
                return null;
            _mapper.Map(curvePointUpdateDto, existingCurvePoint);
            await _curvePointRepository.UpdateAsync(existingCurvePoint);
            return _mapper.Map<CurvePointReadDto>(existingCurvePoint);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var existingCurvePoint = await _curvePointRepository.GetByIdAsync(id);
            if (existingCurvePoint == null)
                return false;
            await _curvePointRepository.DeleteAsync(id);
            return true;
        }
    }
}
