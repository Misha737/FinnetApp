namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Infrastructure;

using EvolutionaryArchitecture.Fitnet.Common.Events;
using System.Text.Json;
using EvolutionaryArchitecture.Fitnet.Common.Events.EventBus;
using Microsoft.EntityFrameworkCore;

internal sealed class OutboxProcessor(IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var persistence = scope.ServiceProvider.GetRequiredService<PassesPersistence>();
                var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();

                var transfer = await persistence.Transfers
                    .Where(t => t.ProcessedAt == null)
                    .OrderBy(t => t.CreatedAt)
                    .FirstOrDefaultAsync(stoppingToken);

                if (transfer is not null)
                {
                    var eventType = Type.GetType(transfer.Type) ?? throw new InvalidOperationException($"Type {transfer.Type} not found.");
                    var integrationEvent = (IIntegrationEvent)JsonSerializer.Deserialize(transfer.Payload, eventType)!;

                    await eventBus.PublishAsync(integrationEvent, stoppingToken);

                    transfer.ProcessedAt = DateTime.UtcNow;
                    await persistence.SaveChangesAsync(stoppingToken);
                }
            }

            await Task.Delay(5000, stoppingToken);
        }
    }
}
