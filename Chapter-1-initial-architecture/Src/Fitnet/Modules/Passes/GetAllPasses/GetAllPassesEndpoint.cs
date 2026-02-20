namespace EvolutionaryArchitecture.Fitnet.Passes.GetAllPasses;

internal static class GetAllPassesEndpoint
{
    internal static void MapGetAllPasses(this IEndpointRouteBuilder app) =>
        app.MapGet(PassesApiPaths.GetAll, async (Application.IPassService passService, CancellationToken cancellationToken) =>
            {
                var response = await passService.GetAllAsync(cancellationToken);

                return Results.Ok(response);
            })
            .WithSummary("Returns all passes that exist in the system")
            .WithDescription("This endpoint is used to retrieve all existing passes.")
            .Produces<GetAllPassesResponse>()
            .Produces(StatusCodes.Status500InternalServerError);
}
