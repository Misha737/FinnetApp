namespace EvolutionaryArchitecture.Fitnet.Passes.RegisterPass
{
    using Contracts.SignContract.Events;
    using EvolutionaryArchitecture.Fitnet.Common.Events;
    using EvolutionaryArchitecture.Fitnet.Common.Events.EventBus;

    internal sealed class ContractSignedEventHandler(Application.IPassService passService, IEventBus eventBus) : IIntegrationEventHandler<ContractSignedEvent>
    {
        public Task Handle(ContractSignedEvent @event, CancellationToken cancellationToken)
        {
            return HandleInternal(@event, cancellationToken);
        }

        private async Task HandleInternal(ContractSignedEvent @event, CancellationToken cancellationToken)
        {
            await passService.RegisterFromContractAsync(@event.ContractCustomerId, @event.SignedAt, @event.ExpireAt, cancellationToken);
            var passes = await passService.GetAllAsync(cancellationToken);
            var created = passes is not null;
            if (created)
            {
                await eventBus.PublishAsync(RegisterPass.Events.PassRegisteredEvent.Create(Guid.NewGuid()), cancellationToken);
            }
        }
    }
}
