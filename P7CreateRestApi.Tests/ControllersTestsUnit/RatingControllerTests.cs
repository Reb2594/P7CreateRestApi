using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs.Rating;
using P7CreateRestApi.Services.Interfaces;
using Xunit;

namespace P7CreateRestApi.Tests.ControllersTestsUnit
{
    public class RatingControllerTests
    {
        private readonly Mock<IRatingService> _mockService;
        private readonly RatingController _controller;

        public RatingControllerTests()
        {
            _mockService = new Mock<IRatingService>();
            _controller = new RatingController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllRatings_RetourneOk()
        {
            // Arrange
            var ratings = new List<RatingReadDto> { new RatingReadDto { Id = 1 } };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(ratings);

            // Act
            var result = await _controller.GetAllRatings();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetRating_RatingExiste_RetourneOk()
        {
            // Arrange
            var rating = new RatingReadDto { Id = 1, MoodysRating = "Aaa" };
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(rating);

            // Act
            var result = await _controller.GetRating(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRating = Assert.IsType<RatingReadDto>(okResult.Value);
            Assert.Equal(1, returnedRating.Id);
        }

        [Fact]
        public async Task GetRating_RatingNexistePas_RetourneNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((RatingReadDto)null);

            // Act
            var result = await _controller.GetRating(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task CreateRating_RetourneOk()
        {
            // Arrange
            var createDto = new RatingCreateDto { MoodysRating = "Aaa" };
            var createdRating = new RatingReadDto { Id = 1, MoodysRating = "Aaa" };
            _mockService.Setup(s => s.CreateAsync(createDto)).ReturnsAsync(createdRating);

            // Act
            var result = await _controller.CreateRating(createDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task UpdateRating_RatingExiste_RetourneOk()
        {
            // Arrange
            var updateDto = new RatingUpdateDto { MoodysRating = "Baa" };
            var updatedRating = new RatingReadDto { Id = 1, MoodysRating = "Baa" };
            _mockService.Setup(s => s.UpdateAsync(1, updateDto)).ReturnsAsync(updatedRating);

            // Act
            var result = await _controller.UpdateRating(1, updateDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateRating_RatingNexistePas_RetourneNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.UpdateAsync(999, It.IsAny<RatingUpdateDto>())).ReturnsAsync((RatingReadDto)null);

            // Act
            var result = await _controller.UpdateRating(999, new RatingUpdateDto());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteRating_RatingExiste_RetourneNoContent()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteRating(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteRating_RatingNexistePas_RetourneNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteAsync(999)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteRating(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}