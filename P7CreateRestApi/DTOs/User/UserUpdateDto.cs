using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs.User
{
    /// <summary>
    /// DTO utilisé pour mettre à jour un utilisateur existant.
    /// Contient uniquement les champs modifiables par le client. (PUT)
    /// </summary>
    public class UserUpdateDto
    {
        [Required(ErrorMessage = "Nom d'utilisateur requis")]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Nom requis")]
        [StringLength(100)]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Rôle requis")]
        public string Role { get; set; }
    }
}
