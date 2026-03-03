namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Domain;

internal interface IPassRepository
{
    Task<Pass?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    Task AddAsync(Pass pass, CancellationToken cancellationToken = default);
}
