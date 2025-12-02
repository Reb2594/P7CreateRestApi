using AutoMapper;
using Microsoft.AspNetCore.Identity;
using P7CreateRestApi.DTOs.User;
using P7CreateRestApi.Services.Interfaces;

namespace P7CreateRestApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AuthService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IJwtService jwtService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<UserReadDto> Register(UserRegisterDto dto)
        {
            var user = new IdentityUser
            {
                UserName = dto.Username,
                Email = dto.Email
            };

            var createResult = await _userManager.CreateAsync(user, dto.Password);

            if (!createResult.Succeeded)
                throw new Exception(string.Join(", ", createResult.Errors.Select(e => e.Description)));

            if (!await _roleManager.RoleExistsAsync(dto.Role))
                throw new Exception($"Le rôle '{dto.Role}' n'existe pas.");

            var roleAssign = await _userManager.AddToRoleAsync(user, dto.Role);
            if (!roleAssign.Succeeded)
                throw new Exception(string.Join(", ", roleAssign.Errors.Select(e => e.Description)));

            var resultDto = _mapper.Map<UserReadDto>(user);
            resultDto.Roles = new List<string> { dto.Role };
            return resultDto;
        }

        public async Task<string> Login(UserLoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

            if (!result.Succeeded)
                throw new Exception("Échec de connexion.");

            var user = await _userManager.FindByNameAsync(dto.Username);
            return await _jwtService.GenerateToken(user);
        }
    }
}
