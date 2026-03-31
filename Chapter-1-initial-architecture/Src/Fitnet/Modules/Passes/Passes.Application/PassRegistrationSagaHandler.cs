namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Application;

using EvolutionaryArchitecture.Fitnet.Common.Events;
using EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Domain;

internal sealed class PassRegistrationSagaHandler(
    ISagaRepository sagaRepository,
    TimeProvider timeProvider) : IIntegrationEventHandler<PassRegisteredEvent>
{
    public async Task Handle(PassRegisteredEvent @event, CancellationToken cancellationToken)
    {
        var saga = await sagaRepository.GetByCorrelationIdAsync(@event.PassId, cancellationToken) ?? throw new InvalidOperationException($"Saga for PassId {@event.PassId} not found.");
        saga.MarkAsCompleted(timeProvider.GetUtcNow().UtcDateTime);

        await sagaRepository.SaveChangesAsync(cancellationToken);
    }
}
