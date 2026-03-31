namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Infrastructure.Sagas;

using EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Domain;
using Microsoft.EntityFrameworkCore;

internal sealed class SagaRepository(PassesPersistence persistence) : ISagaRepository
{
    public async Task AddAsync(PassRegistrationSaga saga, CancellationToken cancellationToken = default) =>
        await persistence.Sagas.AddAsync(saga, cancellationToken);

    public async Task<PassRegistrationSaga?> GetByCorrelationIdAsync(Guid correlationId, CancellationToken cancellationToken = default) =>
        await persistence.Sagas.FirstOrDefaultAsync(s => s.CorrelationId == correlationId, cancellationToken);

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await persistence.SaveChangesAsync(cancellationToken);
}
