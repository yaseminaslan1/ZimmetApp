using Microsoft.EntityFrameworkCore;
using ZimmetApp.Core.Entities;
using ZimmetApp.Core.Enums;
using System.Security.Cryptography;
using System.Text;

namespace ZimmetApp.Infrastructure.Data;

public static class DbSeeder
{
    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + "_ZimmetAppSalt2026"));
        return Convert.ToBase64String(bytes);
    }

    public static async Task SeedAsync(AppDbContext context)
    {
        var adminHash = HashPassword("123456");

        // Mevcut kullanıcılar varsa şifrelerini hash'e güncelle
        var existingUsers = await context.Users.ToListAsync();
        if (existingUsers.Any())
        {
            foreach (var u in existingUsers)
            {
                if (u.PasswordHash == "123456")
                {
                    u.PasswordHash = adminHash;
                }
            }
            await context.SaveChangesAsync();
        }
        else
        {
            var users = new List<User>
            {
                new()
                {
                    FullName = "Yasemin Aslan",
                    Email = "yasemin@kurum.gov.tr",
                    PasswordHash = adminHash,
                    Department = "Bilişim Dairesi",
                    Role = UserRole.Admin
                },
                new()
                {
                    FullName = "Ahmet Yılmaz",
                    Email = "ahmet@kurum.gov.tr",
                    PasswordHash = adminHash,
                    Department = "İstatistik Dairesi",
                    Role = UserRole.Employee
                }
            };
            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }

        if (!await context.Assets.AnyAsync())
        {
            var assets = new List<Asset>
            {
                new() { SerialNumber = "SR-1001", Name = "MacBook Pro 16\"", Category = "Bilgisayar", Cost = 95000, Status = AssetStatus.Assigned },
                new() { SerialNumber = "SR-1002", Name = "Dell Monitor 27\"", Category = "Monitör", Cost = 12500, Status = AssetStatus.InStock },
                new() { SerialNumber = "SR-1003", Name = "Siber Güvenlik Lisansı", Category = "Yazılım Lisansı", Cost = 25000, Status = AssetStatus.Assigned },
                new() { SerialNumber = "SR-1004", Name = "Dell Monitor 27\"", Category = "Monitör", Cost = 12500, Status = AssetStatus.InStock },
                new() { SerialNumber = "SR-1005", Name = "HP LaserJet Yazıcı", Category = "Yazıcı", Cost = 18000, Status = AssetStatus.InMaintenance },
                new() { SerialNumber = "SR-1006", Name = "Lenovo ThinkPad X1", Category = "Bilgisayar", Cost = 72000, Status = AssetStatus.Assigned }
            };

            await context.Assets.AddRangeAsync(assets);
            await context.SaveChangesAsync();

            var assignments = new List<Assignment>
            {
                new()
                {
                    AssetId = assets[0].Id,
                    AssignedToEmployeeName = "Ahmet Yılmaz",
                    EmployeeEmail = "ahmet@kurum.gov.tr",
                    Department = "İstatistik Dairesi",
                    AssignedDate = DateTime.UtcNow.AddMonths(-2),
                    IsActive = true
                },
                new()
                {
                    AssetId = assets[5].Id,
                    AssignedToEmployeeName = "Yasemin Aslan",
                    EmployeeEmail = "yasemin@kurum.gov.tr",
                    Department = "Bilişim Dairesi",
                    AssignedDate = DateTime.UtcNow.AddDays(-5),
                    IsActive = true
                }
            };

            await context.Assignments.AddRangeAsync(assignments);
            await context.SaveChangesAsync();
        }
    }
}
