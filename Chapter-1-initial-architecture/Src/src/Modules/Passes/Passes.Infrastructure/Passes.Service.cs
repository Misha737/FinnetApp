namespace EvolutionaryArchitecture.Fitnet.Passes.Infrastructure
{
    using Application;
    using Data;
    using Data.Database;
    using GetAllPasses;
    using MarkPassAsExpired.Events;
    using RegisterPass.Events;
    using EvolutionaryArchitecture.Fitnet.Common.Events.EventBus;
    using Microsoft.EntityFrameworkCore;

    internal sealed class PassService : IPassService
    {
        private readonly PassesPersistence persistence;

        public PassService(PassesPersistence persistence)
        {
            this.persistence = persistence;
        }

        public async Task<GetAllPassesResponse> GetAllAsync(CancellationToken cancellationToken)
        {
            var passes = await persistence.Passes
                .AsNoTracking()
                .Select(p => PassDto.From(p))
                .ToListAsync(cancellationToken);

            return GetAllPassesResponse.Create(passes);
        }

        public async Task RegisterFromContractAsync(Guid customerId, DateTimeOffset from, DateTimeOffset to, CancellationToken cancellationToken)
        {
            var pass = Data.Pass.Register(customerId, from, to);
            await persistence.Passes.AddAsync(pass, cancellationToken);
            await persistence.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> MarkAsExpiredAsync(Guid id, DateTimeOffset now, CancellationToken cancellationToken)
        {
            var pass = await persistence.Passes.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            if (pass is null)
            {
                return false;
            }

            pass.MarkAsExpired(now);
            await persistence.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
