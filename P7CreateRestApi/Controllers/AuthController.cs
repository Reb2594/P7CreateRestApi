using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Services.Interfaces;
using P7CreateRestApi.DTOs.User;
using Microsoft.AspNetCore.Authorization;

namespace P7CreateRestApi.Controllers
{
    /// <summary>
    /// Contrôleur pour l'authentification et l'inscription des utilisateurs.
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Inscrit un nouvel utilisateur dans le système.
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Register(dto);
            return Ok(new { message = "Utilisateur créé avec succès", user = result });
        }

        /// <summary>
        /// Authentifie un utilisateur et génère un token JWT.
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _authService.Login(dto);

            if (string.IsNullOrEmpty(token))
                return Unauthorized(new { message = "Identifiants invalides" });

            return Ok(new { token, message = "Connexion réussie" });
        }
    }
}