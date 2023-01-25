using Microsoft.EntityFrameworkCore;
using Shared.Persistence;

namespace BankAccounts.Persistence;

public class TransactionsAppDbContext : AppDbContext
{
    public TransactionsAppDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
