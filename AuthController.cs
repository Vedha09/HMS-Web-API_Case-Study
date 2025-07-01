using AmazeCare.DTOs;
using AmazeCare.Models;
using AmazeCare.Repository.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazeCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            var user = new User
            {
                Username = registerDto.Username,
                Password = registerDto.Password,
                Role = registerDto.Role
            };

            var token = _authService.GenerateToken(user);
            var response = new RegisterResponseDTO
            {
                Username = user.Username,
                Role = user.Role,
                Token = token
            };

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var user = await _authService.AuthenticateUserAsync(loginDto.Username, loginDto.Password);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var response = new LoginResponseDTO
            {
                Username = user.Username,
                Role = user.Role,
                Token = _authService.GenerateToken(user)
            };

            return Ok(response);
        }
    }
}
