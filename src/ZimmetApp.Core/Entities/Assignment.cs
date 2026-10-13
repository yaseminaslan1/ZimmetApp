namespace ZimmetApp.Core.Entities;

public class Assignment : BaseEntity
{
    public int AssetId { get; set; }
    public Asset Asset { get; set; } = null!;
    public string AssignedToEmployeeName { get; set; } = string.Empty;
    public string EmployeeEmail { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ReturnDate { get; set; }
    public string? ReturnNotes { get; set; }
    public bool IsActive { get; set; } = true;
}
