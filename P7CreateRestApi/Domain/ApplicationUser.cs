using Microsoft.AspNetCore.Identity;

namespace P7CreateRestApi.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string? Fullname { get; set; }
    }
}