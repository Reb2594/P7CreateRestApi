using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.DTOs.User;

namespace P7CreateRestApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetAll();
        Task<UserReadDto> GetById(string id);
        Task<UserReadDto> Update(string id, UserUpdateDto dto);
        Task<string?> GeneratePasswordResetToken(string userId);
        Task<bool> ResetPassword(string userId, string token, string newPassword);
        Task<bool> ChangePassword(string userId, string currentPassword, string newPassword);
        Task<bool> Delete(string id);
    }
}
