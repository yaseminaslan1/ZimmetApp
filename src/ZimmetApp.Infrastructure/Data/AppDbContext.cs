using Microsoft.EntityFrameworkCore;
using ZimmetApp.Core.Entities;

namespace ZimmetApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<Assignment> Assignments => Set<Assignment>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Asset>().HasQueryFilter(a => !a.IsDeleted);
        modelBuilder.Entity<Assignment>().HasQueryFilter(a => !a.IsDeleted);
        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);

        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.Asset)
            .WithMany(asset => asset.Assignments)
            .HasForeignKey(a => a.AssetId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
