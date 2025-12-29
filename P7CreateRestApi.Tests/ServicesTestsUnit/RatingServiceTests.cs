using AutoMapper;
using Moq;
using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs.Rating;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services;
using Xunit;

namespace P7CreateRestApi.Tests.ServicesTestsUnit
{
    public class RatingServiceTests
    {
        private readonly Mock<IRatingRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly RatingService _service;

        public RatingServiceTests()
        {
            _mockRepository = new Mock<IRatingRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new RatingService(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_RetourneListeRatings()
        {
            // Arrange
            var ratings = new List<Rating> { new Rating { Id = 1, MoodysRating = "Aaa" } };
            var ratingDtos = new List<RatingReadDto> { new RatingReadDto { Id = 1, MoodysRating = "Aaa" } };
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(ratings);
            _mockMapper.Setup(m => m.Map<List<RatingReadDto>>(ratings)).Returns(ratingDtos);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsync_RatingExiste_RetourneRating()
        {
            // Arrange
            var rating = new Rating { Id = 1, MoodysRating = "Aaa" };
            var ratingDto = new RatingReadDto { Id = 1, MoodysRating = "Aaa" };
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(rating);
            _mockMapper.Setup(m => m.Map<RatingReadDto>(rating)).Returns(ratingDto);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_RatingNexistePas_RetourneNull()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Rating)null);

            // Act
            var result = await _service.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_CreeNouveauRating()
        {
            // Arrange
            var createDto = new RatingCreateDto { MoodysRating = "Aaa" };
            var rating = new Rating { MoodysRating = "Aaa" };
            var createdRating = new Rating { Id = 1, MoodysRating = "Aaa" };
            var readDto = new RatingReadDto { Id = 1, MoodysRating = "Aaa" };
            _mockMapper.Setup(m => m.Map<Rating>(createDto)).Returns(rating);
            _mockRepository.Setup(r => r.CreateAsync(rating)).ReturnsAsync(createdRating);
            _mockMapper.Setup(m => m.Map<RatingReadDto>(createdRating)).Returns(readDto);

            // Act
            var result = await _service.CreateAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_RatingExiste_RetourneRatingMisAJour()
        {
            // Arrange
            var updateDto = new RatingUpdateDto { MoodysRating = "Baa" };
            var existingRating = new Rating { Id = 1, MoodysRating = "Aaa" };
            var readDto = new RatingReadDto { Id = 1, MoodysRating = "Baa" };
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingRating);
            _mockMapper.Setup(m => m.Map<RatingReadDto>(existingRating)).Returns(readDto);

            // Act
            var result = await _service.UpdateAsync(1, updateDto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateAsync_RatingNexistePas_RetourneNull()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Rating)null);

            // Act
            var result = await _service.UpdateAsync(999, new RatingUpdateDto());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_RatingExiste_RetourneTrue()
        {
            // Arrange
            var existingRating = new Rating { Id = 1 };
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingRating);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_RatingNexistePas_RetourneFalse()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Rating)null);

            // Act
            var result = await _service.DeleteAsync(999);

            // Assert
            Assert.False(result);
        }
    }
}