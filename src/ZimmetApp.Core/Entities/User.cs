using ZimmetApp.Core.Enums;

namespace ZimmetApp.Core.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty; // Güvenli şifreleme
    public string Department { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Employee;
}
