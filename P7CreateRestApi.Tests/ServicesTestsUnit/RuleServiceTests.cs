using AutoMapper;
using Moq;
using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs.Rule;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services;
using Xunit;

namespace P7CreateRestApi.Tests.ServicesTestsUnit
{
    public class RuleServiceTests
    {
        private readonly Mock<IRuleRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly RuleService _service;

        public RuleServiceTests()
        {
            _mockRepository = new Mock<IRuleRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new RuleService(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_RetourneListeRules()
        {
            // Arrange
            var rules = new List<Rule> { new Rule { Id = 1, Name = "Rule1" } };
            var ruleDtos = new List<RuleReadDto> { new RuleReadDto { Id = 1, Name = "Rule1" } };
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(rules);
            _mockMapper.Setup(m => m.Map<List<RuleReadDto>>(rules)).Returns(ruleDtos);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsync_RuleExiste_RetourneRule()
        {
            // Arrange
            var rule = new Rule { Id = 1, Name = "Rule1" };
            var ruleDto = new RuleReadDto { Id = 1, Name = "Rule1" };
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(rule);
            _mockMapper.Setup(m => m.Map<RuleReadDto>(rule)).Returns(ruleDto);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_RuleNexistePas_RetourneNull()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Rule)null);

            // Act
            var result = await _service.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_CreeNouvelleRule()
        {
            // Arrange
            var createDto = new RuleCreateDto { Name = "NewRule" };
            var rule = new Rule { Name = "NewRule" };
            var createdRule = new Rule { Id = 1, Name = "NewRule" };
            var readDto = new RuleReadDto { Id = 1, Name = "NewRule" };
            _mockMapper.Setup(m => m.Map<Rule>(createDto)).Returns(rule);
            _mockRepository.Setup(r => r.CreateAsync(rule)).ReturnsAsync(createdRule);
            _mockMapper.Setup(m => m.Map<RuleReadDto>(createdRule)).Returns(readDto);

            // Act
            var result = await _service.CreateAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_RuleExiste_RetourneRuleMiseAJour()
        {
            // Arrange
            var updateDto = new RuleUpdateDto { Name = "UpdatedRule" };
            var existingRule = new Rule { Id = 1, Name = "OldRule" };
            var readDto = new RuleReadDto { Id = 1, Name = "UpdatedRule" };
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingRule);
            _mockMapper.Setup(m => m.Map<RuleReadDto>(existingRule)).Returns(readDto);

            // Act
            var result = await _service.UpdateAsync(1, updateDto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateAsync_RuleNexistePas_RetourneNull()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Rule)null);

            // Act
            var result = await _service.UpdateAsync(999, new RuleUpdateDto());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_RuleExiste_RetourneTrue()
        {
            // Arrange
            var existingRule = new Rule { Id = 1 };
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingRule);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_RuleNexistePas_RetourneFalse()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Rule)null);

            // Act
            var result = await _service.DeleteAsync(999);

            // Assert
            Assert.False(result);
        }
    }
}