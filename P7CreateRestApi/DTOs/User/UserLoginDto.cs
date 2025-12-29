using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs.User
{
    /// <summary>
    /// DTO utilisé pour qu'un utilisateur se connecte.
    /// Contient uniquement les champs nécessaires à la connexion. (POST)
    /// </summary>
    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
