using AutoMapper;
using Moq;
using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs.Bid;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services;
using Xunit;

namespace P7CreateRestApi.Tests.ServicesTestsUnit
{
    public class BidServiceTests
    {
        private readonly Mock<IBidRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BidService _service;

        public BidServiceTests()
        {
            _mockRepository = new Mock<IBidRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new BidService(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_RetourneListeOffres()
        {
            // Arrange
            var bids = new List<Bid> { new Bid { BidId = 1, Account = "Test" } };
            var bidDtos = new List<BidReadDto> { new BidReadDto { BidId = 1, Account = "Test" } };
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(bids);
            _mockMapper.Setup(m => m.Map<List<BidReadDto>>(bids)).Returns(bidDtos);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsync_OffreExiste_RetourneOffre()
        {
            // Arrange
            var bid = new Bid { BidId = 1, Account = "Test" };
            var bidDto = new BidReadDto { BidId = 1, Account = "Test" };
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(bid);
            _mockMapper.Setup(m => m.Map<BidReadDto>(bid)).Returns(bidDto);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.BidId);
        }

        [Fact]
        public async Task GetByIdAsync_OffreNexistePas_RetourneNull()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Bid)null);

            // Act
            var result = await _service.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_CreeNouvelleOffre()
        {
            // Arrange
            var createDto = new BidCreateDto { Account = "New" };
            var bid = new Bid { Account = "New" };
            var createdBid = new Bid { BidId = 1, Account = "New" };
            var readDto = new BidReadDto { BidId = 1, Account = "New" };
            _mockMapper.Setup(m => m.Map<Bid>(createDto)).Returns(bid);
            _mockRepository.Setup(r => r.CreateAsync(bid)).ReturnsAsync(createdBid);
            _mockMapper.Setup(m => m.Map<BidReadDto>(createdBid)).Returns(readDto);

            // Act
            var result = await _service.CreateAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.BidId);
        }

        [Fact]
        public async Task UpdateAsync_OffreExiste_RetourneOffreMiseAJour()
        {
            // Arrange
            var updateDto = new BidUpdateDto { Account = "Updated" };
            var existingBid = new Bid { BidId = 1, Account = "Old" };
            var readDto = new BidReadDto { BidId = 1, Account = "Updated" };
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingBid);
            _mockMapper.Setup(m => m.Map<BidReadDto>(existingBid)).Returns(readDto);

            // Act
            var result = await _service.UpdateAsync(1, updateDto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateAsync_OffreNexistePas_RetourneNull()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Bid)null);

            // Act
            var result = await _service.UpdateAsync(999, new BidUpdateDto());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_OffreExiste_RetourneTrue()
        {
            // Arrange
            var existingBid = new Bid { BidId = 1 };
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingBid);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_OffreNexistePas_RetourneFalse()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Bid)null);

            // Act
            var result = await _service.DeleteAsync(999);

            // Assert
            Assert.False(result);
        }
    }
}