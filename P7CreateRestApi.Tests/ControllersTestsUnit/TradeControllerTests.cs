using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs.Trade;
using P7CreateRestApi.Services.Interfaces;
using Xunit;

namespace P7CreateRestApi.Tests.ControllersTestsUnit
{
    public class TradeControllerTests
    {
        private readonly Mock<ITradeService> _mockService;
        private readonly TradeController _controller;

        public TradeControllerTests()
        {
            _mockService = new Mock<ITradeService>();
            _controller = new TradeController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllTrades_RetourneOk()
        {
            // Arrange
            var trades = new List<TradeReadDto> { new TradeReadDto { TradeId = 1 } };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(trades);

            // Act
            var result = await _controller.GetAllTrades();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetTrade_TradeExiste_RetourneOk()
        {
            // Arrange
            var trade = new TradeReadDto { TradeId = 1, Account = "Account1" };
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(trade);

            // Act
            var result = await _controller.GetTrade(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTrade = Assert.IsType<TradeReadDto>(okResult.Value);
            Assert.Equal(1, returnedTrade.TradeId);
        }

        [Fact]
        public async Task GetTrade_TradeNexistePas_RetourneNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((TradeReadDto)null);

            // Act
            var result = await _controller.GetTrade(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task CreateTrade_RetourneOk()
        {
            // Arrange
            var createDto = new TradeCreateDto { Account = "NewAccount" };
            var createdTrade = new TradeReadDto { TradeId = 1, Account = "NewAccount" };
            _mockService.Setup(s => s.CreateAsync(createDto)).ReturnsAsync(createdTrade);

            // Act
            var result = await _controller.CreateTrade(createDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task UpdateTrade_TradeExiste_RetourneOk()
        {
            // Arrange
            var updateDto = new TradeUpdateDto { Account = "UpdatedAccount" };
            var updatedTrade = new TradeReadDto { TradeId = 1, Account = "UpdatedAccount" };
            _mockService.Setup(s => s.UpdateAsync(1, updateDto)).ReturnsAsync(updatedTrade);

            // Act
            var result = await _controller.UpdateTrade(1, updateDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateTrade_TradeNexistePas_RetourneNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.UpdateAsync(999, It.IsAny<TradeUpdateDto>())).ReturnsAsync((TradeReadDto)null);

            // Act
            var result = await _controller.UpdateTrade(999, new TradeUpdateDto());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteTrade_TradeExiste_RetourneNoContent()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteTrade(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteTrade_TradeNexistePas_RetourneNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteAsync(999)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteTrade(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}