namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Infrastructure;

using EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class TransferEntityConfiguration : IEntityTypeConfiguration<Transfer>
{
    public void Configure(EntityTypeBuilder<Transfer> builder)
    {
        builder.ToTable("Transfers");
        builder.HasKey(transfers => transfers.Id);
        builder.Property(transfers => transfers.Type).IsRequired();
        builder.Property(transfers => transfers.Payload).IsRequired();
        builder.Property(transfers => transfers.CreatedAt).IsRequired();
        builder.Property(transfers => transfers.ProcessedAt);
    }
}
