using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs.User
{
    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
