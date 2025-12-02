using Microsoft.AspNetCore.Identity;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateToken(IdentityUser user);
    }

}
