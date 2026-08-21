using ShoppingAgent.Application.AI;

namespace ShoppingAgent.Application.Abstractions
{
    public interface IConversationSummarizer
    {
        Task<string> SummarizeAsync(
            IReadOnlyList<ChatMessageDto> messages,
            CancellationToken cancellationToken = default);
    }
}
