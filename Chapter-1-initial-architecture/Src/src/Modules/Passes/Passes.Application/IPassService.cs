namespace EvolutionaryArchitecture.Fitnet.Passes.Application
{
    using GetAllPasses;

    public interface IPassService
    {
        Task<GetAllPassesResponse> GetAllAsync(CancellationToken cancellationToken);
        Task RegisterFromContractAsync(Guid customerId, DateTimeOffset from, DateTimeOffset to, CancellationToken cancellationToken);
        Task<bool> MarkAsExpiredAsync(Guid id, DateTimeOffset now, CancellationToken cancellationToken);
    }
}
