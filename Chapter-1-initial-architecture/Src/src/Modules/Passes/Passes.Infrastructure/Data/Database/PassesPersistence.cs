namespace EvolutionaryArchitecture.Fitnet.Passes.Data.Database
{
    using Microsoft.EntityFrameworkCore;

    internal sealed class PassesPersistence(DbContextOptions<PassesPersistence> options) : DbContext(options)
    {
        private const string Schema = "Passes";

        public DbSet<Data.Pass> Passes => Set<Data.Pass>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            modelBuilder.ApplyConfiguration(new PassEntityConfiguration());
        }
    }
}
