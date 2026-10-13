using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZimmetApp.Application.DTOs;
using ZimmetApp.Core.Entities;
using ZimmetApp.Core.Enums;
using ZimmetApp.Infrastructure.Data;

namespace ZimmetApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssignmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public AssignmentsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignAsset([FromBody] CreateAssignmentDto dto)
    {
        var asset = await _context.Assets.FindAsync(dto.AssetId);
        if (asset == null) return NotFound(new { message = "Varlık bulunamadı." });

        if (asset.Status == AssetStatus.Assigned)
            return BadRequest(new { message = "Bu cihaz zaten başka bir personele zimmetli!" });

        var assignment = new Assignment
        {
            AssetId = dto.AssetId,
            AssignedToEmployeeName = dto.AssignedToEmployeeName,
            EmployeeEmail = dto.EmployeeEmail,
            Department = dto.Department,
            AssignedDate = DateTime.UtcNow,
            IsActive = true
        };

        asset.Status = AssetStatus.Assigned;

        await _context.Assignments.AddAsync(assignment);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Zimmet ataması başarıyla yapıldı." });
    }

    [HttpPost("return/{assetId}")]
    public async Task<IActionResult> ReturnAsset(int assetId)
    {
        var asset = await _context.Assets.FindAsync(assetId);
        if (asset == null) return NotFound(new { message = "Varlık bulunamadı." });

        var assignment = await _context.Assignments
            .FirstOrDefaultAsync(a => a.AssetId == assetId && a.IsActive);

        if (assignment != null)
        {
            assignment.IsActive = false;
            assignment.ReturnDate = DateTime.UtcNow;
            assignment.ReturnNotes = "Depoya iade edildi.";
        }

        asset.Status = AssetStatus.InStock;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Cihaz başarıyla depoya iade alındı." });
    }
}
