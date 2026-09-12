using ShoppingAgent.Application.AI;

namespace ShoppingAgent.Application.Abstractions
{
    public interface IConversationSummarizer
    {
        Task<string> SummarizeAsync(
             string? previousSummary,
            IReadOnlyList<ChatMessageDto> messages,
            CancellationToken cancellationToken = default);
    }
}
