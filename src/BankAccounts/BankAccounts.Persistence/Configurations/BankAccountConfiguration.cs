using BankAccounts.Domain.Entities;
using BankAccounts.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankAccounts.Persistence.Configurations;

internal sealed class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.ToTable(TableNames.BankAccounts);

        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.Transactions)
            .WithOne()
            .HasForeignKey(x => x.BankAccountId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
