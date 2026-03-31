namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Domain;

internal interface ISagaRepository
{
    Task AddAsync(PassRegistrationSaga saga, CancellationToken cancellationToken = default);
    Task<PassRegistrationSaga?> GetByCorrelationIdAsync(Guid correlationId, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
