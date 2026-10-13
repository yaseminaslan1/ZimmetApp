using ZimmetApp.Core.Enums;

namespace ZimmetApp.Application.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public int ActiveAssignmentsCount { get; set; }
}

public class CreateUserDto
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = "123456"; // Kurumsal varsayılan şifre
    public string Department { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Employee;
}
