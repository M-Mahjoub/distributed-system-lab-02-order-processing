using ShoppingAgent.Application.AI;

namespace ShoppingAgent.Application.Abstractions
{
    public interface IConversationRepository
    {
        Task<Conversation?> GetAsync(
            string id,
            CancellationToken cancellationToken = default);

        Task CreateAsync(
            Conversation conversation,
            CancellationToken cancellationToken = default);

        Task<bool> AppendMessageAsync(
            string conversationId,
            ChatMessageDto message,
            CancellationToken cancellationToken = default);

        Task UpdateSummaryAsync(
            string conversationId,
            string summary,
            CancellationToken cancellationToken = default);
    }
}
