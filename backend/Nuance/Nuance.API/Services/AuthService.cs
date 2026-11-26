using Microsoft.AspNetCore.Identity;
using Nuance.API.DTOs;
using Nuance.API.Services.Interfaces;
using Nuance.API.Models;
using Nuance.API.Data;
using Microsoft.AspNetCore.Authentication;

namespace Nuance.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly NuanceDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(UserManager<ApplicationUser> userManager, NuanceDbContext context, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IdentityResult> RegisterAsync(RegisterDTO registerDTO)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            var user = new ApplicationUser
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
            {
                return result;
            }
            var wardrobe = new Wardrobe
            {
                UserId = user.Id,
                Name = $"{user.UserName}'s Wardrobe"
            };

            _context.Wardrobes.Add(wardrobe);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return IdentityResult.Success;
        }
        public async Task<bool> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                return false;
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return false;
            }
            var httpContext = _httpContextAccessor.HttpContext
                ?? throw new InvalidOperationException("HttpContext is not available.");
            httpContext.Response.StatusCode = StatusCodes.Status200OK;
            httpContext.Response.ContentType = "application/json";
            await httpContext.SignInAsync(IdentityConstants.BearerScheme, await _signInManager.CreateUserPrincipalAsync(user));
            return true;
        }
    }
}
