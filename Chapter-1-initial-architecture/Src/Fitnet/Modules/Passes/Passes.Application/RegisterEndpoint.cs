namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Application.EventHandlers;

using EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Domain;
using EvolutionaryArchitecture.Fitnet.Contracts.SignContract.Events;
using EvolutionaryArchitecture.Fitnet.Common.Events;
using EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Application;
using System.Text.Json;

internal sealed class ContractSignedEventHandler(
    IPassRepository passRepository,
    ISagaRepository sagaRepository) : IIntegrationEventHandler<ContractSignedEvent>
{
    public async Task Handle(ContractSignedEvent @event, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var pass = Pass.Register(@event.ContractCustomerId, @event.SignedAt, @event.ExpireAt);

        var passRegisteredEvent = PassRegisteredEvent.Create(pass.Id);
        var transfer = Transfer.Register(typeof(PassRegisteredEvent).AssemblyQualifiedName!, JsonSerializer.Serialize(passRegisteredEvent, passRegisteredEvent.GetType()), now);

        var saga = PassRegistrationSaga.Start(pass.Id, now);

        await passRepository.AddAsync(pass, transfer, cancellationToken);
        await sagaRepository.AddAsync(saga, cancellationToken);

        await passRepository.SaveChangesAsync(cancellationToken);
    }
}
