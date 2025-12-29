using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs.Bid;
using P7CreateRestApi.Services.Interfaces;
using Xunit;

namespace P7CreateRestApi.Tests.ControllersTestsUnit
{
    public class BidControllerTests
    {
        private readonly Mock<IBidService> _mockService;
        private readonly BidController _controller;

        public BidControllerTests()
        {
            _mockService = new Mock<IBidService>();
            _controller = new BidController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllBids_RetourneOk()
        {
            // Arrange
            var bids = new List<BidReadDto> { new BidReadDto { BidId = 1 } };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(bids);

            // Act
            var result = await _controller.GetAllBids();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetBid_OffreExiste_RetourneOk()
        {
            // Arrange
            var bid = new BidReadDto { BidId = 1, Account = "Test" };
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(bid);

            // Act
            var result = await _controller.GetBid(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBid = Assert.IsType<BidReadDto>(okResult.Value);
            Assert.Equal(1, returnedBid.BidId);
        }

        [Fact]
        public async Task GetBid_OffreNexistePas_RetourneNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((BidReadDto)null);

            // Act
            var result = await _controller.GetBid(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task CreateBid_RetourneOk()
        {
            // Arrange
            var createDto = new BidCreateDto { Account = "New" };
            var createdBid = new BidReadDto { BidId = 1, Account = "New" };
            _mockService.Setup(s => s.CreateAsync(createDto)).ReturnsAsync(createdBid);

            // Act
            var result = await _controller.CreateBid(createDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task UpdateBid_OffreExiste_RetourneOk()
        {
            // Arrange
            var updateDto = new BidUpdateDto { Account = "Updated" };
            var updatedBid = new BidReadDto { BidId = 1, Account = "Updated" };
            _mockService.Setup(s => s.UpdateAsync(1, updateDto)).ReturnsAsync(updatedBid);

            // Act
            var result = await _controller.UpdateBid(1, updateDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateBid_OffreNexistePas_RetourneNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.UpdateAsync(999, It.IsAny<BidUpdateDto>())).ReturnsAsync((BidReadDto)null);

            // Act
            var result = await _controller.UpdateBid(999, new BidUpdateDto());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteBid_OffreExiste_RetourneNoContent()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteBid(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteBid_OffreNexistePas_RetourneNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteAsync(999)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteBid(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}