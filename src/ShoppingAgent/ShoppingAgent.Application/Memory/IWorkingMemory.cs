using ShoppingAgent.Application.Memory;

public interface IWorkingMemory
{
    Task<WorkingMemory?> GetAsync(
        string conversationId,
        CancellationToken cancellationToken = default);

    Task SaveAsync(
        string conversationId,
        WorkingMemory memory,
        CancellationToken cancellationToken = default);
}