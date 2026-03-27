namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Infrastructure;

using EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Domain;
using Microsoft.EntityFrameworkCore;

internal sealed class PassesPersistence(DbContextOptions<PassesPersistence> options) : DbContext(options)
{
    private const string Schema = "Passes";

    public DbSet<Pass> Passes => Set<Pass>();
    public DbSet<Transfer> Transfers => Set<Transfer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new PassEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TransferEntityConfiguration());
    }
}
