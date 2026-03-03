namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Presentation;

using EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Application;

internal static class MarkPassAsExpiredEndpoint
{
    internal static void MapMarkPassAsExpired(this IEndpointRouteBuilder app) => app.MapPatch(
            PassesApiPaths.MarkPassAsExpired,
            async (
                Guid id,
                MarkPassAsExpiredCommandHandler handler,
                CancellationToken cancellationToken) =>
            {
                try
                {
                    var command = new MarkPassAsExpiredCommand(id);
                    await handler.Handle(command, cancellationToken);

                    return Results.NoContent();
                }
                catch (Exception)
                {
                    return Results.NotFound();
                }
            })
        .WithSummary("Marks pass which expired");
}
