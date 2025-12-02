using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Services.Interfaces;
using P7CreateRestApi.DTOs.User;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
                return NotFound(new { Message = "Utilisateur introuvable." });

            return Ok(user);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UserUpdateDto dto)
        {
            var result = await _userService.Update(id, dto);
            if (result == null)
                return NotFound(new { Message = "Utilisateur introuvable." });

            return Ok(result);
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _userService.Delete(id);
            if (!deleted)
                return NotFound(new { Message = "Utilisateur introuvable." });

            return Ok(new { Message = "Utilisateur supprimé." });
        }

        [HttpPost("{userId}/forgot-password")]
        public async Task<IActionResult> GenerateResetToken(string userId)
        {
            var token = await _userService.GeneratePasswordResetToken(userId);

            if (token == null)
                return NotFound(new { Message = "Utilisateur introuvable." });

            // En prod, renvoyer le token par email
            return Ok(new { ResetToken = token });
        }

        [HttpPost("{userId}/reset-password")]
        public async Task<IActionResult> ResetPassword(string userId, ResetPasswordDto dto)
        {
            var result = await _userService.ResetPassword(userId, dto.Token, dto.NewPassword);

            if (!result)
                return BadRequest(new { Message = "Échec du changement de mot de passe. Token invalide ou mot de passe non conforme." });

            return Ok(new { Message = "Mot de passe réinitialisé avec succès." });
        }

        [HttpPost("{userId}/change-password")]
        public async Task<IActionResult> ChangePassword(string userId, ChangePasswordDto dto)
        {
            var result = await _userService.ChangePassword(userId, dto.CurrentPassword, dto.NewPassword);

            if (!result)
                return BadRequest(new { Message = "Échec du changement de mot de passe. L’ancien mot de passe est incorrect ou le nouveau est invalide." });

            return Ok(new { Message = "Mot de passe modifié avec succès." });
        }
    }
}
