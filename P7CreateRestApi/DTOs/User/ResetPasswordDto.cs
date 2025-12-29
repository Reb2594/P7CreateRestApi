using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs.User
{
    /// <summary>
    /// DTO utilisé pour changer le mot de passe d'un utilisateur.
    /// Contient le token de réinitialisation et le nouveau mot de passe.
    /// </summary>
    public class ResetPasswordDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 12)]
        public string NewPassword { get; set; }
    }
}
