namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Domain;

public record RegisterPassRequest(Guid CustomerId, DateTimeOffset From, DateTimeOffset To);
