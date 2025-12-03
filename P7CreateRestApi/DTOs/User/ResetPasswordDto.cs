using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs.User
{
    public class ResetPasswordDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 12)]
        public string NewPassword { get; set; }
    }
}
