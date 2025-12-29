using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs.User
{
    /// <summary>
    /// DTO utilisé pour enregistrer un nouvel utilisateur.
    /// Contient uniquement les champs à renseigner par le client. (POST)
    /// </summary>
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Nom d'utilisateur requis")]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email requis")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mot de passe requis")]
        [StringLength(100, MinimumLength = 12)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Nom requis")]
        [StringLength(100)]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Rôle requis")]
        public string Role { get; set; }
    }

}
