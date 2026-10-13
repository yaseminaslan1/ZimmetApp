using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZimmetApp.Application.DTOs;
using ZimmetApp.Application.Services;
using ZimmetApp.Core.Entities;
using ZimmetApp.Core.Enums;
using ZimmetApp.Infrastructure.Data;

namespace ZimmetApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IAuthService _authService;

    public UsersController(AppDbContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _context.Users
            .Select(u => new UserDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Department = u.Department,
                Role = u.Role,
                ActiveAssignmentsCount = _context.Assignments.Count(a => a.IsActive && a.EmployeeEmail == u.Email)
            })
            .ToListAsync();

        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email.ToLower() == dto.Email.ToLower()))
            return BadRequest(new { message = "Bu e-posta adresiyle kayıtlı bir personel zaten var!" });

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = _authService.HashPassword(dto.Password),
            Department = dto.Department,
            Role = dto.Role
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Ok(user);
    }

    // Kurumsal İlişik Kesme (Off-boarding)
    [HttpPost("offboard/{email}")]
    public async Task<IActionResult> OffboardUser(string email)
    {
        var activeAssignments = await _context.Assignments
            .Include(a => a.Asset)
            .Where(a => a.EmployeeEmail.ToLower() == email.ToLower() && a.IsActive)
            .ToListAsync();

        if (!activeAssignments.Any())
            return Ok(new { message = "Personelin üzerinde aktif zimmet bulunmamaktadır. İlişik kesme temiz.", returnedCount = 0 });

        foreach (var assignment in activeAssignments)
        {
            assignment.IsActive = false;
            assignment.ReturnDate = DateTime.UtcNow;
            assignment.ReturnNotes = "Personel işten ayrılışı (İlişik Kesme) sebebiyle toplu iade alındı.";

            if (assignment.Asset != null)
            {
                assignment.Asset.Status = AssetStatus.InStock;
            }
        }

        await _context.SaveChangesAsync();
        return Ok(new { message = $"{activeAssignments.Count} adet envanter başarıyla depoya iade edildi ve personelin ilişiği kesildi.", returnedCount = activeAssignments.Count });
    }
}
