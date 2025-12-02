using P7CreateRestApi.DTOs.User;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserReadDto> Register(UserRegisterDto dto);
        Task<string> Login(UserLoginDto dto);
    }
}
