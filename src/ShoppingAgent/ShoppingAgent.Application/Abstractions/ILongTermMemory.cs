using ShoppingAgent.Application.Memory;

public interface ILongTermMemory
{
    Task SaveAsync(
        string userId,
        MemoryItem item,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<MemoryItem>> SearchAsync(
        string userId,
        string query,
        CancellationToken cancellationToken = default);
}