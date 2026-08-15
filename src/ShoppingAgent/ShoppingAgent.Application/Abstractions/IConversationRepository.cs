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

        Task ReplaceAsync(
            Conversation conversation,
            CancellationToken cancellationToken = default);
    }
}
