using AutoMapper;
using Moq;
using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs.Trade;
using P7CreateRestApi.Services;
using Xunit;

namespace P7CreateRestApi.Tests.ServicesTestsUnit
{
    public class TradeServiceTests
    {
        private readonly Mock<ITradeRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly TradeService _service;

        public TradeServiceTests()
        {
            _mockRepository = new Mock<ITradeRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new TradeService(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_RetourneListeTrades()
        {
            // Arrange
            var trades = new List<Trade> { new Trade { TradeId = 1, Account = "Account1" } };
            var tradeDtos = new List<TradeReadDto> { new TradeReadDto { TradeId = 1, Account = "Account1" } };
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(trades);
            _mockMapper.Setup(m => m.Map<List<TradeReadDto>>(trades)).Returns(tradeDtos);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsync_TradeExiste_RetourneTrade()
        {
            // Arrange
            var trade = new Trade { TradeId = 1, Account = "Account1" };
            var tradeDto = new TradeReadDto { TradeId = 1, Account = "Account1" };
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(trade);
            _mockMapper.Setup(m => m.Map<TradeReadDto>(trade)).Returns(tradeDto);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.TradeId);
        }

        [Fact]
        public async Task GetByIdAsync_TradeNexistePas_RetourneNull()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Trade)null);

            // Act
            var result = await _service.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_CreeNouvelleTrade()
        {
            // Arrange
            var createDto = new TradeCreateDto { Account = "NewAccount" };
            var trade = new Trade { Account = "NewAccount" };
            var createdTrade = new Trade { TradeId = 1, Account = "NewAccount" };
            var readDto = new TradeReadDto { TradeId = 1, Account = "NewAccount" };
            _mockMapper.Setup(m => m.Map<Trade>(createDto)).Returns(trade);
            _mockRepository.Setup(r => r.CreateAsync(trade)).ReturnsAsync(createdTrade);
            _mockMapper.Setup(m => m.Map<TradeReadDto>(createdTrade)).Returns(readDto);

            // Act
            var result = await _service.CreateAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.TradeId);
        }

        [Fact]
        public async Task UpdateAsync_TradeExiste_RetourneTradeMiseAJour()
        {
            // Arrange
            var updateDto = new TradeUpdateDto { Account = "UpdatedAccount" };
            var existingTrade = new Trade { TradeId = 1, Account = "OldAccount" };
            var readDto = new TradeReadDto { TradeId = 1, Account = "UpdatedAccount" };
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingTrade);
            _mockMapper.Setup(m => m.Map<TradeReadDto>(existingTrade)).Returns(readDto);

            // Act
            var result = await _service.UpdateAsync(1, updateDto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateAsync_TradeNexistePas_RetourneNull()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Trade)null);

            // Act
            var result = await _service.UpdateAsync(999, new TradeUpdateDto());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_TradeExiste_RetourneTrue()
        {
            // Arrange
            var existingTrade = new Trade { TradeId = 1 };
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingTrade);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_TradeNexistePas_RetourneFalse()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Trade)null);

            // Act
            var result = await _service.DeleteAsync(999);

            // Assert
            Assert.False(result);
        }
    }
}