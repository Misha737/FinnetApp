namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Presentation;

internal static class PassesApiPaths
{
    internal const string GetAll = $"{ApiPaths.Root}/passes";
    internal const string MarkPassAsExpired = $"{ApiPaths.Root}/passes/{{id}}";
}
