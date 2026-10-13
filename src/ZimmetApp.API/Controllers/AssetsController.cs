using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZimmetApp.Application.DTOs;
using ZimmetApp.Core.Entities;
using ZimmetApp.Core.Enums;
using ZimmetApp.Infrastructure.Data;

namespace ZimmetApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetsController : ControllerBase
{
    private readonly AppDbContext _context;

    public AssetsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var assets = await _context.Assets
            .Include(a => a.Assignments.Where(asg => asg.IsActive))
            .Select(a => new AssetDto
            {
                Id = a.Id,
                SerialNumber = a.SerialNumber,
                Name = a.Name,
                Category = a.Category,
                Cost = a.Cost,
                Status = a.Status,
                AssignedEmployeeName = a.Assignments.FirstOrDefault(asg => asg.IsActive) != null 
                    ? a.Assignments.FirstOrDefault(asg => asg.IsActive)!.AssignedToEmployeeName 
                    : null
            })
            .ToListAsync();

        return Ok(assets);
    }

    [HttpGet("{id}/history")]
    public async Task<IActionResult> GetHistory(int id)
    {
        var asset = await _context.Assets.FindAsync(id);
        if (asset == null) return NotFound(new { message = "Varlık bulunamadı." });

        var history = await _context.Assignments
            .Where(a => a.AssetId == id)
            .OrderByDescending(a => a.AssignedDate)
            .Select(a => new
            {
                a.Id,
                a.AssignedToEmployeeName,
                a.EmployeeEmail,
                a.Department,
                a.AssignedDate,
                a.ReturnDate,
                a.ReturnNotes,
                a.IsActive
            })
            .ToListAsync();

        return Ok(new
        {
            Asset = new { asset.Id, asset.SerialNumber, asset.Name, asset.Category, asset.Status, asset.CreatedDate },
            History = history
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAssetDto dto)
    {
        var asset = new Asset
        {
            SerialNumber = dto.SerialNumber,
            Name = dto.Name,
            Category = dto.Category,
            Cost = dto.Cost,
            Status = AssetStatus.InStock
        };

        await _context.Assets.AddAsync(asset);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { id = asset.Id }, asset);
    }
}
