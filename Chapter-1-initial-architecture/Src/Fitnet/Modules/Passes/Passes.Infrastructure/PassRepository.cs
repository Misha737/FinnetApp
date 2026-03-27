namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Infrastructure;

using EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Domain;

internal sealed class PassRepository(PassesPersistence persistence) : IPassRepository
{
    public async Task<Pass?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await persistence.Passes.FindAsync([id], cancellationToken);

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await persistence.SaveChangesAsync(cancellationToken);

    public async Task AddAsync(Pass pass, Transfer transfer, CancellationToken cancellationToken = default)
    {
        await persistence.Passes.AddAsync(pass, cancellationToken);
        await persistence.Transfers.AddAsync(transfer, cancellationToken);
    }
}
