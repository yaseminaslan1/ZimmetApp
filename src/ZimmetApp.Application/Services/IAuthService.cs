using ZimmetApp.Core.Entities;

namespace ZimmetApp.Application.Services;

public interface IAuthService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
    string GenerateJwtToken(User user, string secretKey, string issuer, string audience);
}
