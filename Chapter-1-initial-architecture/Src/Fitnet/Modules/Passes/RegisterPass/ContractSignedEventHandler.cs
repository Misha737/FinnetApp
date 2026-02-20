namespace EvolutionaryArchitecture.Fitnet.Passes.RegisterPass;

using Contracts.SignContract.Events;
using EvolutionaryArchitecture.Fitnet.Common.Events;

internal sealed class ContractSignedEventHandler(Application.IPassService passService) : IIntegrationEventHandler<ContractSignedEvent>
{
    public Task Handle(ContractSignedEvent @event, CancellationToken cancellationToken) =>
        passService.RegisterFromContractAsync(@event.ContractCustomerId, @event.SignedAt, @event.ExpireAt, cancellationToken);
}
