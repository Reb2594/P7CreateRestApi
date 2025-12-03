using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Services.Interfaces;
using P7CreateRestApi.DTOs.User;
using Microsoft.AspNetCore.Authorization;

namespace P7CreateRestApi.Controllers
{
    /// <summary>
    /// Contrôleur pour la gestion des utilisateurs.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Récupère tous les utilisateurs.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        /// <summary>
        /// Récupère un utilisateur par son identifiant.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
                return NotFound(new { message = $"Utilisateur {id} introuvable" });

            return Ok(user);
        }

        /// <summary>
        /// Met à jour un utilisateur existant.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UserUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Update(id, dto);
            if (result == null)
                return NotFound(new { message = $"Utilisateur {id} introuvable" });

            return Ok(result);
        }

        /// <summary>
        /// Supprime un utilisateur par son identifiant.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _userService.Delete(id);
            if (!deleted)
                return NotFound(new { message = $"Utilisateur {id} introuvable" });

            return NoContent();
        }

        /// <summary>
        /// Génère un token de réinitialisation de mot de passe.
        /// </summary>
        [HttpPost("{id}/forgot-password")]
        public async Task<IActionResult> GenerateResetToken(string id)
        {
            var token = await _userService.GeneratePasswordResetToken(id);
            if (token == null)
                return NotFound(new { message = $"Utilisateur {id} introuvable" });

            return Ok(new { resetToken = token, message = "Token généré avec succès" });
        }

        /// <summary>
        /// Réinitialise le mot de passe avec un token.
        /// </summary>
        [HttpPost("{id}/reset-password")]
        public async Task<IActionResult> ResetPassword(string id, [FromBody] ResetPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.ResetPassword(id, dto.Token, dto.NewPassword);
            if (!result)
                return BadRequest(new { message = "Token invalide ou mot de passe non conforme" });

            return Ok(new { message = "Mot de passe réinitialisé avec succès" });
        }

        /// <summary>
        /// Change le mot de passe d'un utilisateur.
        /// </summary>
        [HttpPost("{id}/change-password")]
        public async Task<IActionResult> ChangePassword(string id, [FromBody] ChangePasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.ChangePassword(id, dto.CurrentPassword, dto.NewPassword);
            if (!result)
                return BadRequest(new { message = "Ancien mot de passe incorrect ou nouveau mot de passe invalide" });

            return Ok(new { message = "Mot de passe modifié avec succès" });
        }
    }
}