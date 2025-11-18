using P7CreateRestApi.Services.Interfaces;
using P7CreateRestApi.DTOs.Rating;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Domain;
using AutoMapper;

namespace P7CreateRestApi.Services
{
    public class RatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public RatingService(IRatingRepository ratingRepository, ILogger logger, IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<List<RatingReadDto>> GetAllAsync()
        {
            var ratings = await _ratingRepository.GetAllAsync();
            return _mapper.Map<List<RatingReadDto>>(ratings);
        }
        public async Task<RatingReadDto?> GetByIdAsync(int id)
        {
            var rating = await _ratingRepository.GetByIdAsync(id);
            return rating == null ? null : _mapper.Map<RatingReadDto>(rating);
        }

        public async Task<RatingReadDto> CreateAsync(RatingCreateDto dto)
        {
            var rating = _mapper.Map<Rating>(dto);
            var createdRating = await _ratingRepository.CreateAsync(rating);
            return _mapper.Map<RatingReadDto>(createdRating);
        }

        public async Task<RatingReadDto?> UpdateAsync(int id, RatingUpdateDto dto)
        {
            var existingRating = await _ratingRepository.GetByIdAsync(id);
            if (existingRating == null)
                return null;
            _mapper.Map(dto, existingRating);
            await _ratingRepository.UpdateAsync(existingRating);
            return _mapper.Map<RatingReadDto>(existingRating);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingRating = await _ratingRepository.GetByIdAsync(id);
            if (existingRating == null)
                return false;
            await _ratingRepository.DeleteAsync(id);
            return true;
        }

    }
}
