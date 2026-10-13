using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZimmetApp.Application.DTOs;
using ZimmetApp.Application.Services;
using ZimmetApp.Infrastructure.Data;

namespace ZimmetApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;

    public AuthController(AppDbContext context, IAuthService authService, IConfiguration configuration)
    {
        _context = context;
        _authService = authService;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower());

        if (user == null || !_authService.VerifyPassword(request.Password, user.PasswordHash))
            return Unauthorized(new { message = "E-posta veya şifre hatalı!" });

        var secret = _configuration["Jwt:Key"] ?? "KurumsalZimmetYonetimSistemiGizliAnahtari2026EnAz32Karakter!";
        var issuer = _configuration["Jwt:Issuer"] ?? "ZimmetApp";
        var audience = _configuration["Jwt:Audience"] ?? "ZimmetAppClients";

        var token = _authService.GenerateJwtToken(user, secret, issuer, audience);

        return Ok(new LoginResponseDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Department = user.Department,
            Role = user.Role,
            Token = token
        });
    }
}
