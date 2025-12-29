using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs.User;
using P7CreateRestApi.Services.Interfaces;
using Xunit;

namespace P7CreateRestApi.Tests.ControllersTestsUnit
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockService;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockService = new Mock<IUserService>();
            _controller = new UserController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_RetourneOk()
        {
            // Arrange
            var users = new List<UserReadDto>
            {
                new UserReadDto { Id = "1", Username = "User1" }
            };
            _mockService.Setup(s => s.GetAll()).ReturnsAsync(users);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetById_UserExiste_RetourneOk()
        {
            // Arrange
            var user = new UserReadDto { Id = "1", Username = "TestUser" };
            _mockService.Setup(s => s.GetById("1")).ReturnsAsync(user);

            // Act
            var result = await _controller.GetById("1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<UserReadDto>(okResult.Value);
            Assert.Equal("1", returnedUser.Id);
        }

        [Fact]
        public async Task GetById_UserNexistePas_RetourneNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GetById("999")).ReturnsAsync((UserReadDto)null);

            // Act
            var result = await _controller.GetById("999");

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Update_UserExiste_RetourneOk()
        {
            // Arrange
            var updateDto = new UserUpdateDto { Username = "UpdatedUser" };
            var updatedUser = new UserReadDto { Id = "1", Username = "UpdatedUser" };
            _mockService.Setup(s => s.Update("1", updateDto)).ReturnsAsync(updatedUser);

            // Act
            var result = await _controller.Update("1", updateDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Update_UserNexistePas_RetourneNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.Update("999", It.IsAny<UserUpdateDto>())).ReturnsAsync((UserReadDto)null);

            // Act
            var result = await _controller.Update("999", new UserUpdateDto());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Update_ModelStateInvalide_RetourneBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("Username", "Le nom d'utilisateur est requis");

            // Act
            var result = await _controller.Update("1", new UserUpdateDto());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            _mockService.Verify(s => s.Update(It.IsAny<string>(), It.IsAny<UserUpdateDto>()), Times.Never);
        }

        [Fact]
        public async Task Delete_UserExiste_RetourneNoContent()
        {
            // Arrange
            _mockService.Setup(s => s.Delete("1")).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete("1");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_UserNexistePas_RetourneNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.Delete("999")).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete("999");

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GenerateResetToken_UserExiste_RetourneOk()
        {
            // Arrange
            var token = "reset-token-123";
            _mockService.Setup(s => s.GeneratePasswordResetToken("1")).ReturnsAsync(token);

            // Act
            var result = await _controller.GenerateResetToken("1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GenerateResetToken_UserNexistePas_RetourneNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GeneratePasswordResetToken("999")).ReturnsAsync((string)null);

            // Act
            var result = await _controller.GenerateResetToken("999");

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task ResetPassword_TokenValide_RetourneOk()
        {
            // Arrange
            var resetDto = new ResetPasswordDto { Token = "valid-token", NewPassword = "NewPass123!" };
            _mockService.Setup(s => s.ResetPassword("1", resetDto.Token, resetDto.NewPassword)).ReturnsAsync(true);

            // Act
            var result = await _controller.ResetPassword("1", resetDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ResetPassword_TokenInvalide_RetourneBadRequest()
        {
            // Arrange
            var resetDto = new ResetPasswordDto { Token = "invalid-token", NewPassword = "NewPass123!" };
            _mockService.Setup(s => s.ResetPassword("1", resetDto.Token, resetDto.NewPassword)).ReturnsAsync(false);

            // Act
            var result = await _controller.ResetPassword("1", resetDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ResetPassword_ModelStateInvalide_RetourneBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("NewPassword", "Le mot de passe est requis");

            // Act
            var result = await _controller.ResetPassword("1", new ResetPasswordDto());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            _mockService.Verify(s => s.ResetPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task ChangePassword_MotDePasseValide_RetourneOk()
        {
            // Arrange
            var changeDto = new ChangePasswordDto { CurrentPassword = "OldPass123!", NewPassword = "NewPass123!" };
            _mockService.Setup(s => s.ChangePassword("1", changeDto.CurrentPassword, changeDto.NewPassword)).ReturnsAsync(true);

            // Act
            var result = await _controller.ChangePassword("1", changeDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ChangePassword_AncienMotDePasseIncorrect_RetourneBadRequest()
        {
            // Arrange
            var changeDto = new ChangePasswordDto { CurrentPassword = "WrongPass!", NewPassword = "NewPass123!" };
            _mockService.Setup(s => s.ChangePassword("1", changeDto.CurrentPassword, changeDto.NewPassword)).ReturnsAsync(false);

            // Act
            var result = await _controller.ChangePassword("1", changeDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ChangePassword_ModelStateInvalide_RetourneBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("NewPassword", "Le mot de passe est requis");

            // Act
            var result = await _controller.ChangePassword("1", new ChangePasswordDto());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            _mockService.Verify(s => s.ChangePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}