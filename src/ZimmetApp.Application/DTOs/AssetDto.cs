using ZimmetApp.Core.Enums;

namespace ZimmetApp.Application.DTOs;

public class AssetDto
{
    public int Id { get; set; }
    public string SerialNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public AssetStatus Status { get; set; }
    public string? AssignedEmployeeName { get; set; }
}

public class CreateAssetDto
{
    public string SerialNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Cost { get; set; }
}
