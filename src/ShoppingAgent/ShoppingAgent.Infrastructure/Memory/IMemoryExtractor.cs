using ShoppingAgent.Application.AI;

namespace ShoppingAgent.Application.Memory;

public interface IMemoryExtractor
{
    Task<IReadOnlyList<MemoryItem>> ExtractAsync(
        string userId,
        IReadOnlyList<ChatMessageDto> messages,
        CancellationToken cancellationToken = default);
}