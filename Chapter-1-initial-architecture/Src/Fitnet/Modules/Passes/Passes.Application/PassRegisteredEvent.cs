namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Application;

using EvolutionaryArchitecture.Fitnet.Common.Events;

internal record PassRegisteredEvent(Guid Id, Guid PassId, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    internal static PassRegisteredEvent Create(Guid passId) =>
        new(Guid.NewGuid(), passId, DateTimeOffset.UtcNow);
}
