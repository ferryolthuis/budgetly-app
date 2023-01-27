using BankAccounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Persistence;

namespace BankAccounts.Persistence;

public sealed class BankAccountsAppDbContext : AppDbContext
{
    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();
    
    public DbSet<Transaction> Transactions => Set<Transaction>();
    
    public BankAccountsAppDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
