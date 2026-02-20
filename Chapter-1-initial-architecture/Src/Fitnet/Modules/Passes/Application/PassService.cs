namespace EvolutionaryArchitecture.Fitnet.Passes.Application;

using Data;
using Data.Database;
using GetAllPasses;
using MarkPassAsExpired.Events;
using RegisterPass.Events;
using EvolutionaryArchitecture.Fitnet.Common.Events.EventBus;
using Microsoft.EntityFrameworkCore;

internal sealed class PassService(PassesPersistence persistence, IEventBus eventBus, TimeProvider timeProvider) : IPassService
{
    public async Task<GetAllPassesResponse> GetAllAsync(CancellationToken cancellationToken)
    {
        var passes = await persistence.Passes
            .AsNoTracking()
            .Select(p => PassDto.From(p))
            .ToListAsync(cancellationToken);

        return GetAllPassesResponse.Create(passes);
    }

    public async Task RegisterFromContractAsync(Guid customerId, DateTimeOffset from, DateTimeOffset to, CancellationToken cancellationToken)
    {
        var pass = Pass.Register(customerId, from, to);
        await persistence.Passes.AddAsync(pass, cancellationToken);
        await persistence.SaveChangesAsync(cancellationToken);

        var passRegisteredEvent = PassRegisteredEvent.Create(pass.Id);
        await eventBus.PublishAsync(passRegisteredEvent, cancellationToken);
    }

    public async Task<bool> MarkAsExpiredAsync(Guid id, CancellationToken cancellationToken)
    {
        var pass = await persistence.Passes.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (pass is null)
        {
            return false;
        }

        var now = timeProvider.GetUtcNow();
        pass.MarkAsExpired(now);
        await persistence.SaveChangesAsync(cancellationToken);

        await eventBus.PublishAsync(PassExpiredEvent.Create(pass.Id, pass.CustomerId, timeProvider.GetUtcNow()), cancellationToken);

        return true;
    }
}
