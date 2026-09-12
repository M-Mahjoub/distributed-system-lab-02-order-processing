namespace ShoppingAgent.Application.Memory;

public sealed class MemoryItem
{
    public Guid Id { get; init; }

    public string UserId { get; init; } = string.Empty;

    public string Type { get; init; } = string.Empty;

    public string Content { get; init; } = string.Empty;

    public DateTimeOffset CreatedAt { get; init; }

    public DateTimeOffset? UpdatedAt { get; set; }
}