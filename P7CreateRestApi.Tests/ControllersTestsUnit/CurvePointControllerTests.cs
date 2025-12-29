using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs.CurvePoint;
using P7CreateRestApi.Services.Interfaces;
using Xunit;

namespace P7CreateRestApi.Tests.ControllersTestsUnit
{
    public class CurvePointControllerTests
    {
        private readonly Mock<ICurvePointService> _mockService;
        private readonly CurvePointController _controller;

        public CurvePointControllerTests()
        {
            _mockService = new Mock<ICurvePointService>();
            _controller = new CurvePointController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllCurvePoints_RetourneOk()
        {
            // Arrange
            var curvePoints = new List<CurvePointReadDto> { new CurvePointReadDto { Id = 1 } };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(curvePoints);

            // Act
            var result = await _controller.GetAllCurvePoints();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetCurvePoint_CurvePointExiste_RetourneOk()
        {
            // Arrange
            var curvePoint = new CurvePointReadDto { Id = 1, CurveId = 10 };
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(curvePoint);

            // Act
            var result = await _controller.GetCurvePoint(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCurvePoint = Assert.IsType<CurvePointReadDto>(okResult.Value);
            Assert.Equal(1, returnedCurvePoint.Id);
        }

        [Fact]
        public async Task GetCurvePoint_CurvePointNexistePas_RetourneNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((CurvePointReadDto)null);

            // Act
            var result = await _controller.GetCurvePoint(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task CreateCurvePoint_RetourneOk()
        {
            // Arrange
            var createDto = new CurvePointCreateDto { CurveId = 10 };
            var createdCurvePoint = new CurvePointReadDto { Id = 1, CurveId = 10 };
            _mockService.Setup(s => s.CreateAsync(createDto)).ReturnsAsync(createdCurvePoint);

            // Act
            var result = await _controller.CreateCurvePoint(createDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task UpdateCurvePoint_CurvePointExiste_RetourneOk()
        {
            // Arrange
            var updateDto = new CurvePointUpdateDto { CurveId = 20 };
            var updatedCurvePoint = new CurvePointReadDto { Id = 1, CurveId = 20 };
            _mockService.Setup(s => s.UpdateAsync(1, updateDto)).ReturnsAsync(updatedCurvePoint);

            // Act
            var result = await _controller.UpdateCurvePoint(1, updateDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateCurvePoint_CurvePointNexistePas_RetourneNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.UpdateAsync(999, It.IsAny<CurvePointUpdateDto>())).ReturnsAsync((CurvePointReadDto)null);

            // Act
            var result = await _controller.UpdateCurvePoint(999, new CurvePointUpdateDto());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCurvePoint_CurvePointExiste_RetourneNoContent()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteCurvePoint(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCurvePoint_CurvePointNexistePas_RetourneNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteAsync(999)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteCurvePoint(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}