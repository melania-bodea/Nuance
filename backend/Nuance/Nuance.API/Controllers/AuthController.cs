using Microsoft.AspNetCore.Mvc;
using Nuance.API.DTOs;
using Nuance.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Nuance.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var result = await _authService.RegisterAsync(dto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return StatusCode(201, new
            {
                Message = "User registered successfully."
            });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var succeeded = await _authService.LoginAsync(dto);
            if (!succeeded)
            {
                return Unauthorized(new { Message = "Invalid credentials." });
            }
            return new EmptyResult();
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.Identity?.Name;

            return Ok(new { Id = userId, Username = username });
        }
        }
    }