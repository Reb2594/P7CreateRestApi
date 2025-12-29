using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs.User;
using P7CreateRestApi.Services.Interfaces;
using Xunit;

namespace P7CreateRestApi.Tests.ControllersTestsUnit
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _mockService;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockService = new Mock<IAuthService>();
            _controller = new AuthController(_mockService.Object);
        }

        [Fact]
        public async Task Register_DonneesValides_RetourneOk()
        {
            // Arrange
            var registerDto = new UserRegisterDto
            {
                Username = "testuser",
                Email = "test@test.com",
                Password = "Test123!",
                Fullname = "Test User",
                Role = "User"
            };
            var userReadDto = new UserReadDto
            {
                Id = "1",
                Username = "testuser",
                Email = "test@test.com"
            };
            _mockService.Setup(s => s.Register(registerDto)).ReturnsAsync(userReadDto);

            // Act
            var result = await _controller.Register(registerDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task Register_ModelStateInvalide_RetourneBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("Username", "Le nom d'utilisateur est requis");
            var registerDto = new UserRegisterDto();

            // Act
            var result = await _controller.Register(registerDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            _mockService.Verify(s => s.Register(It.IsAny<UserRegisterDto>()), Times.Never);
        }

        [Fact]
        public async Task Register_ServiceLeveException_PropageException()
        {
            // Arrange
            var registerDto = new UserRegisterDto
            {
                Username = "testuser",
                Email = "test@test.com",
                Password = "Test123!",
                Role = "InvalidRole"
            };
            _mockService.Setup(s => s.Register(registerDto))
                .ThrowsAsync(new Exception("Le rôle 'InvalidRole' n'existe pas."));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _controller.Register(registerDto));
        }

        [Fact]
        public async Task Login_CredentialsValides_RetourneOkAvecToken()
        {
            // Arrange
            var loginDto = new UserLoginDto
            {
                Username = "testuser",
                Password = "Test123!"
            };
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...";
            _mockService.Setup(s => s.Login(loginDto)).ReturnsAsync(token);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task Login_CredentialsInvalides_RetourneUnauthorized()
        {
            // Arrange
            var loginDto = new UserLoginDto
            {
                Username = "wronguser",
                Password = "wrongpass"
            };
            _mockService.Setup(s => s.Login(loginDto)).ReturnsAsync(string.Empty);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.NotNull(unauthorizedResult.Value);
        }

        [Fact]
        public async Task Login_ModelStateInvalide_RetourneBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("Username", "Le nom d'utilisateur est requis");
            var loginDto = new UserLoginDto();

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            _mockService.Verify(s => s.Login(It.IsAny<UserLoginDto>()), Times.Never);
        }

        [Fact]
        public async Task Login_ServiceLeveException_PropageException()
        {
            // Arrange
            var loginDto = new UserLoginDto
            {
                Username = "testuser",
                Password = "wrongpassword"
            };
            _mockService.Setup(s => s.Login(loginDto))
                .ThrowsAsync(new Exception("Échec de connexion."));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _controller.Login(loginDto));
        }
    }
}