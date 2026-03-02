namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Presentation;


internal static class PassesEndpoints
{
    internal static void MapPasses(this IEndpointRouteBuilder app)
    {
        app.MapGetAllPasses();
        app.MapMarkPassAsExpired();
    }
}
