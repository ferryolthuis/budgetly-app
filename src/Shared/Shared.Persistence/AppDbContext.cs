using Microsoft.EntityFrameworkCore;

namespace Shared.Persistence;

public abstract class AppDbContext : DbContext
{
    protected AppDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
}
