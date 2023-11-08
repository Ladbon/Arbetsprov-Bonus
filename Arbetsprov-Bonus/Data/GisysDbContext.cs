using Arbetsprov_Bonus.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arbetsprov_Bonus.Data;

public class GisysDbContext : DbContext
{
    public GisysDbContext(DbContextOptions options) : base(options)
    {
    }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Define the Consultant entity's key and property configurations
    modelBuilder.Entity<Consultant>(entity =>
    {
        entity.HasKey(c => c.Id);
        entity.Property(c => c.Id).ValueGeneratedOnAdd();
        entity.Property(c => c.FirstName).IsRequired().HasMaxLength(50);
        entity.Property(c => c.LastName).IsRequired().HasMaxLength(50);
        entity.Property(c => c.StartDate).IsRequired();
    });
}

    public DbSet<Consultant> Consultants => Set<Consultant>();
}
