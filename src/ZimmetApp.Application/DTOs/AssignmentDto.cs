namespace ZimmetApp.Application.DTOs;

public class CreateAssignmentDto
{
    public int AssetId { get; set; }
    public string AssignedToEmployeeName { get; set; } = string.Empty;
    public string EmployeeEmail { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
}

public class ReturnAssignmentDto
{
    public int AssignmentId { get; set; }
    public string? ReturnNotes { get; set; }
}
