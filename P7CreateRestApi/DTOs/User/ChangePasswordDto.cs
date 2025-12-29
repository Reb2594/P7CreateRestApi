using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs.User
{
    /// <summary>
    /// DTO utilisé pour changer le mot de passe d'un utilisateur.
    /// Contient le mot de passe actuel et le nouveau mot de passe.
    /// </summary>
    public class ChangePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 12)]
        public string NewPassword { get; set; }
    }
}
