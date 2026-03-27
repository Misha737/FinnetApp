namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Application.EventHandlers;

using EvolutionaryArchitecture.Fitnet.Common.Events.EventBus;
using EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Domain;
using EvolutionaryArchitecture.Fitnet.Contracts.SignContract.Events;
using EvolutionaryArchitecture.Fitnet.Common.Events;
using EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Application;

internal sealed class ContractSignedEventHandler(
    IPassRepository passRepository,
    IEventBus eventBus) : IIntegrationEventHandler<ContractSignedEvent>
{
    public async Task Handle(ContractSignedEvent @event, CancellationToken cancellationToken)
    {
        var pass = Pass.Register(@event.ContractCustomerId, @event.SignedAt, @event.ExpireAt);
        // For test purpose
        var transfer = Transfer.Register("some_type", "Some message", DateTime.UtcNow);

        await passRepository.AddAsync(pass, transfer, cancellationToken);
        await passRepository.SaveChangesAsync(cancellationToken);

        var passRegisteredEvent = PassRegisteredEvent.Create(pass.Id);
        await eventBus.PublishAsync(passRegisteredEvent, cancellationToken);
    }
}
