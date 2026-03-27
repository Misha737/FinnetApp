namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Domain;

internal sealed class Transfer
{
    public Guid Id { get; init; }
    public string Type { get; init; }
    public string Payload { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? ProcessedAt { get; set; }

    private Transfer(Guid id, string type, string payload, DateTime createdAt, DateTime? processedAt)
    {
        Id = id;
        Type = type;
        Payload = payload;
        CreatedAt = createdAt;
        ProcessedAt = processedAt;
    }

    internal static Transfer Register(string type, string payload, DateTime CreateAt)
        => new(Guid.NewGuid(), type, payload, CreateAt, null);

}
