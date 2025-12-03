using Microsoft.AspNetCore.Identity;
using P7CreateRestApi.Services.Interfaces;
using AutoMapper;
using P7CreateRestApi.DTOs.User;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserReadDto>> GetAll()
        {
            var users = _userManager.Users.ToList();
            var userDtos = new List<UserReadDto>();

            foreach (var user in users)
            {
                var dto = _mapper.Map<UserReadDto>(user);
                var roles = await _userManager.GetRolesAsync(user);
                dto.Roles = roles;
                userDtos.Add(dto);
            }

            return userDtos;
        }

        public async Task<UserReadDto?> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            var dto = _mapper.Map<UserReadDto>(user);
            dto.Roles = await _userManager.GetRolesAsync(user);
            return dto;
        }

        public async Task<UserReadDto> Update(string id, UserUpdateDto dto)
        {
            var existingUser = await _userManager.FindByIdAsync(id);
            if (existingUser == null)
                return null;

            _mapper.Map(dto, existingUser);

            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
                throw new Exception("Échec de la mise à jour de l'utilisateur");

            // Gestion des rôles
            var currentRoles = await _userManager.GetRolesAsync(existingUser);
            if (!string.IsNullOrEmpty(dto.Role))
            {
                await _userManager.RemoveFromRolesAsync(existingUser, currentRoles);
                await _userManager.AddToRoleAsync(existingUser, dto.Role);
            }

            var dtoResult = _mapper.Map<UserReadDto>(existingUser);
            dtoResult.Roles = await _userManager.GetRolesAsync(existingUser);

            return dtoResult;
        }

        public async Task<bool> Delete(string id)
        {
            var existingUser = await _userManager.FindByIdAsync(id);
            if (existingUser == null)
                return false;

            var result = await _userManager.DeleteAsync(existingUser);
            return result.Succeeded;
        }

        public async Task<string?> GeneratePasswordResetToken(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<bool> ResetPassword(string userId, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            // Identity valide automatiquement le mot de passe selon la configuration du Program.cs
            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            return result.Succeeded;
        }
    }
}
