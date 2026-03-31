namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Domain;

internal enum SagaStatus
{
    Started = 1,
    Completed = 2,
    Failed = 3
}

internal sealed class PassRegistrationSaga
{
    public Guid Id { get; private set; }

    public Guid CorrelationId { get; private set; }
    public SagaStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private PassRegistrationSaga() { }

    private PassRegistrationSaga(Guid correlationId, DateTime createdAt)
    {
        Id = Guid.NewGuid();
        CorrelationId = correlationId;
        Status = SagaStatus.Started;
        CreatedAt = createdAt;
    }

    internal static PassRegistrationSaga Start(Guid correlationId, DateTime createdAt)
        => new(correlationId, createdAt);

    internal void MarkAsCompleted(DateTime updatedAt)
    {
        if (Status != SagaStatus.Started)
        {
            throw new InvalidOperationException($"Cannot complete saga in state {Status}");
        }

        Status = SagaStatus.Completed;
        UpdatedAt = updatedAt;
    }

    internal void MarkAsFailed(DateTime updatedAt)
    {
        Status = SagaStatus.Failed;
        UpdatedAt = updatedAt;
    }
}
