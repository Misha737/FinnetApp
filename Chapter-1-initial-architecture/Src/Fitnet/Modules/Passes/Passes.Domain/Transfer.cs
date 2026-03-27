namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Domain;

internal class Transfer
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public string Payload { get; set; }
    public DateTime CreatedAt { get; set; }
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
