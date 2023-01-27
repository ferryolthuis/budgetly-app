using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Persistence.Constants;
using Shared.Persistence.Outbox;

namespace Shared.Persistence.Configuration;

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable(CommonTableNames.OutboxMessages);

        builder.HasKey(x => x.Id);
    }
}
