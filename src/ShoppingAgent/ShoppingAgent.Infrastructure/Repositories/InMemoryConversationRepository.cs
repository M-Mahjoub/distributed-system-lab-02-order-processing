using ShoppingAgent.Application.Abstractions;
using ShoppingAgent.Application.AI;

namespace ShoppingAgent.Infrastructure.Repositories
{
    public sealed class InMemoryConversationRepository
    : IConversationRepository
    {
        private readonly Dictionary<string, Conversation> _store = [];

        public Task<Conversation?> GetAsync(
            string id,
            CancellationToken cancellationToken = default)
        {
            _store.TryGetValue(
                id,
                out var conversation);

            return Task.FromResult(conversation);
        }

        public Task CreateAsync(
            Conversation conversation,
            CancellationToken cancellationToken = default)
        {
            if (_store.ContainsKey(conversation.Id))
            {
                throw new InvalidOperationException(
                    $"Conversation '{conversation.Id}' already exists.");
            }

            _store[conversation.Id] = conversation;

            return Task.CompletedTask;
        }

        public Task ReplaceAsync(
            Conversation conversation,
            CancellationToken cancellationToken = default)
        {
            _store[conversation.Id] = conversation;

            return Task.CompletedTask;
        }

        public Task<bool> AppendMessageAsync(string conversationId, ChatMessageDto message, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSummaryAsync(string conversationId, string summary, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
