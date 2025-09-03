using E_CommerceSystem.Models;

namespace E_CommerceSystem.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
        RefreshToken GenerateRefreshToken(User user);
        User ValidateRefreshToken(string token);
    }
}
