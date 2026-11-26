using Microsoft.AspNetCore.Identity;
using Nuance.API.DTOs;

namespace Nuance.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterDTO registerDTO);
        Task<bool> LoginAsync(LoginDTO loginDTO);
    }
}
