namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Application;

using EvolutionaryArchitecture.Fitnet.Common.Events.EventBus;
using EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Domain;

internal sealed record MarkPassAsExpiredCommand(Guid Id);

public sealed class PassNotFoundException(Guid id)
    : Exception($"Pass with ID {id} was not found.");

internal sealed class MarkPassAsExpiredCommandHandler(
    IPassRepository repository,
    TimeProvider timeProvider,
    IEventBus eventBus)
{

    public async Task Handle(MarkPassAsExpiredCommand command, CancellationToken cancellationToken)
    {
        var pass = await repository.GetByIdAsync(command.Id, cancellationToken) ?? throw new PassNotFoundException(command.Id);
        var nowDate = timeProvider.GetUtcNow();

        pass.MarkAsExpired(nowDate);

        await repository.SaveChangesAsync(cancellationToken);

        await eventBus.PublishAsync(
            PassExpiredEvent.Create(pass.Id, pass.CustomerId, nowDate),
            cancellationToken);
    }
}
