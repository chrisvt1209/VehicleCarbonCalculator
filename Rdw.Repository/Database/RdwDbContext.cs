using Microsoft.EntityFrameworkCore;
using Rdw.Repository.Entities;

namespace Rdw.Repository.Database;

public class RdwDbContext(DbContextOptions<RdwDbContext> options) : DbContext(options)
{
    public DbSet<VehicleEntity> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(RdwDbContext).Assembly);
}
