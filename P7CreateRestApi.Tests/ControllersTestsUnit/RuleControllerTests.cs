using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs.Rule;
using P7CreateRestApi.Services.Interfaces;
using Xunit;

namespace P7CreateRestApi.Tests.ControllersTestsUnit
{
    public class RuleControllerTests
    {
        private readonly Mock<IRuleService> _mockService;
        private readonly RuleController _controller;

        public RuleControllerTests()
        {
            _mockService = new Mock<IRuleService>();
            _controller = new RuleController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllRules_RetourneOk()
        {
            // Arrange
            var rules = new List<RuleReadDto> { new RuleReadDto { Id = 1 } };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(rules);

            // Act
            var result = await _controller.GetAllRules();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetRule_RuleExiste_RetourneOk()
        {
            // Arrange
            var rule = new RuleReadDto { Id = 1, Name = "Rule1" };
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(rule);

            // Act
            var result = await _controller.GetRule(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRule = Assert.IsType<RuleReadDto>(okResult.Value);
            Assert.Equal(1, returnedRule.Id);
        }

        [Fact]
        public async Task GetRule_RuleNexistePas_RetourneNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((RuleReadDto)null);

            // Act
            var result = await _controller.GetRule(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task CreateRule_RetourneOk()
        {
            // Arrange
            var createDto = new RuleCreateDto { Name = "NewRule" };
            var createdRule = new RuleReadDto { Id = 1, Name = "NewRule" };
            _mockService.Setup(s => s.CreateAsync(createDto)).ReturnsAsync(createdRule);

            // Act
            var result = await _controller.CreateRule(createDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task UpdateRule_RuleExiste_RetourneOk()
        {
            // Arrange
            var updateDto = new RuleUpdateDto { Name = "UpdatedRule" };
            var updatedRule = new RuleReadDto { Id = 1, Name = "UpdatedRule" };
            _mockService.Setup(s => s.UpdateAsync(1, updateDto)).ReturnsAsync(updatedRule);

            // Act
            var result = await _controller.UpdateRule(1, updateDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateRule_RuleNexistePas_RetourneNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.UpdateAsync(999, It.IsAny<RuleUpdateDto>())).ReturnsAsync((RuleReadDto)null);

            // Act
            var result = await _controller.UpdateRule(999, new RuleUpdateDto());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteRule_RuleExiste_RetourneNoContent()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteRule(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteRule_RuleNexistePas_RetourneNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteAsync(999)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteRule(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}