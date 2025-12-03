using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs.User
{
    public class ChangePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 12)]
        public string NewPassword { get; set; }
    }
}
