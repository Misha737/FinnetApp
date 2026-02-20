namespace EvolutionaryArchitecture.Fitnet.Passes.Application;

using GetAllPasses;

internal interface IPassService
{
    Task<GetAllPassesResponse> GetAllAsync(CancellationToken cancellationToken);
    Task RegisterFromContractAsync(Guid customerId, DateTimeOffset from, DateTimeOffset to, CancellationToken cancellationToken);
    Task<bool> MarkAsExpiredAsync(Guid id, CancellationToken cancellationToken);
}
