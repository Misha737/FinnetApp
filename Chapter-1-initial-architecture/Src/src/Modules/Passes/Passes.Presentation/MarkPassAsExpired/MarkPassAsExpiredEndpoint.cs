namespace EvolutionaryArchitecture.Fitnet.Passes.MarkPassAsExpired
{
    internal static class MarkPassAsExpiredEndpoint
    {
        internal static void MapMarkPassAsExpired(this IEndpointRouteBuilder app) => app.MapPatch(
                PassesApiPaths.MarkPassAsExpired,
                async (
                    Guid id,
                    Application.IPassService passService,
                    EvolutionaryArchitecture.Fitnet.Common.Clock.TimeProvider timeProvider,
                    EvolutionaryArchitecture.Fitnet.Common.Events.EventBus.IEventBus eventBus,
                    CancellationToken cancellationToken) =>
                {
                    var nowDate = timeProvider.GetUtcNow();
                    var result = await passService.MarkAsExpiredAsync(id, nowDate, cancellationToken);
                    if (!result)
                    {
                        return Results.NotFound();
                    }

                    await eventBus.PublishAsync(PassExpiredEvent.Create(id, Guid.Empty, nowDate), cancellationToken);

                    return Results.NoContent();
                })
            .WithSummary("Marks pass which expired")
            .WithDescription("This endpoint is used to mark expired pass. Based on that it is possible to offer new contract to customer.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
