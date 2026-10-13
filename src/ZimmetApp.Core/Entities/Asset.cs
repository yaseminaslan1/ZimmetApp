using ZimmetApp.Core.Enums;

namespace ZimmetApp.Core.Entities;

public class Asset : BaseEntity
{
    public string SerialNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public AssetStatus Status { get; set; } = AssetStatus.InStock;
    public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
}
