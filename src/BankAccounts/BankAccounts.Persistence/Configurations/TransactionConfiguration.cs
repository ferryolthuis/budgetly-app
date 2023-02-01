using BankAccounts.Domain.Entities;
using BankAccounts.Domain.ValueObjects;
using BankAccounts.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankAccounts.Persistence.Configurations;

internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable(TableNames.Transactions);

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Inflow)
            .HasConversion(x => x.Value, v => Money.FromAmount(v).Value);
        
        builder
            .Property(x => x.Outflow)
            .HasConversion(x => x.Value, v => Money.FromAmount(v).Value);
    }
}
