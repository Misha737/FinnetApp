namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Infrastructure;

using EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class PassRegistrationSagaConfiguration : IEntityTypeConfiguration<PassRegistrationSaga>
{
    public void Configure(EntityTypeBuilder<PassRegistrationSaga> builder)
    {
        builder.ToTable("Sagas");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.CorrelationId).IsUnique();
        builder.Property(x => x.Status).IsRequired();
    }
}
