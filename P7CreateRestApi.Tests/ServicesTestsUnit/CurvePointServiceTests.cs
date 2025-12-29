using AutoMapper;
using Moq;
using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs.CurvePoint;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services;
using Xunit;

namespace P7CreateRestApi.Tests.ServicesTestsUnit
{
    public class CurvePointServiceTests
    {
        private readonly Mock<ICurvePointRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CurvePointService _service;

        public CurvePointServiceTests()
        {
            _mockRepository = new Mock<ICurvePointRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new CurvePointService(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_RetourneListeCurvePoints()
        {
            // Arrange
            var curvePoints = new List<CurvePoint> { new CurvePoint { Id = 1, CurveId = 10 } };
            var curvePointDtos = new List<CurvePointReadDto> { new CurvePointReadDto { Id = 1, CurveId = 10 } };
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(curvePoints);
            _mockMapper.Setup(m => m.Map<List<CurvePointReadDto>>(curvePoints)).Returns(curvePointDtos);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsync_CurvePointExiste_RetourneCurvePoint()
        {
            // Arrange
            var curvePoint = new CurvePoint { Id = 1, CurveId = 10 };
            var curvePointDto = new CurvePointReadDto { Id = 1, CurveId = 10 };
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(curvePoint);
            _mockMapper.Setup(m => m.Map<CurvePointReadDto>(curvePoint)).Returns(curvePointDto);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_CurvePointNexistePas_RetourneNull()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((CurvePoint)null);

            // Act
            var result = await _service.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_CreeNouveauCurvePoint()
        {
            // Arrange
            var createDto = new CurvePointCreateDto { CurveId = 10 };
            var curvePoint = new CurvePoint { CurveId = 10 };
            var createdCurvePoint = new CurvePoint { Id = 1, CurveId = 10 };
            var readDto = new CurvePointReadDto { Id = 1, CurveId = 10 };
            _mockMapper.Setup(m => m.Map<CurvePoint>(createDto)).Returns(curvePoint);
            _mockRepository.Setup(r => r.CreateAsync(curvePoint)).ReturnsAsync(createdCurvePoint);
            _mockMapper.Setup(m => m.Map<CurvePointReadDto>(createdCurvePoint)).Returns(readDto);

            // Act
            var result = await _service.CreateAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_CurvePointExiste_RetourneCurvePointMisAJour()
        {
            // Arrange
            var updateDto = new CurvePointUpdateDto { CurveId = 20 };
            var existingCurvePoint = new CurvePoint { Id = 1, CurveId = 10 };
            var readDto = new CurvePointReadDto { Id = 1, CurveId = 20 };
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingCurvePoint);
            _mockMapper.Setup(m => m.Map<CurvePointReadDto>(existingCurvePoint)).Returns(readDto);

            // Act
            var result = await _service.UpdateAsync(1, updateDto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateAsync_CurvePointNexistePas_RetourneNull()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((CurvePoint)null);

            // Act
            var result = await _service.UpdateAsync(999, new CurvePointUpdateDto());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_CurvePointExiste_RetourneTrue()
        {
            // Arrange
            var existingCurvePoint = new CurvePoint { Id = 1 };
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingCurvePoint);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_CurvePointNexistePas_RetourneFalse()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((CurvePoint)null);

            // Act
            var result = await _service.DeleteAsync(999);

            // Assert
            Assert.False(result);
        }
    }
}