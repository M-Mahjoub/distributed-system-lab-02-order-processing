using ShoppingAgent.Application.AI;

namespace ShoppingAgent.Infrastructure.Repositories
{
    using System.Text.Json;
    using ShoppingAgent.Application.Abstractions;
    using ShoppingAgent.Application.AI;
    using StackExchange.Redis;

    using System.Text.Json;
    using StackExchange.Redis;

    public sealed class RedisConversationRepository
        : IConversationRepository
    {
        private readonly IDatabase _database;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = false
        };

        public RedisConversationRepository(
            IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<Conversation?> GetAsync(
            string id,
            CancellationToken cancellationToken = default)
        {
            var key = GetKey(id);

            var value =
                await _database.StringGetAsync(key);

            if (value.IsNullOrEmpty)
            {
                return null;
            }

            var messages =
                JsonSerializer.Deserialize<List<ChatMessageDto>>(
                    value.ToString(),
                    JsonOptions);

            if (messages is null)
            {
                return null;
            }

            return new Conversation(
                id,
                messages);
        }

        public async Task CreateAsync(
            Conversation conversation,
            CancellationToken cancellationToken = default)
        {
            var key = GetKey(conversation.Id);

            var json =
                JsonSerializer.Serialize(
                    conversation.Messages,
                    JsonOptions);

            var created =
                await _database.StringSetAsync(
                    key,
                    json,
                    TimeSpan.FromDays(30),
                    When.NotExists);

            if (!created)
            {
                throw new InvalidOperationException(
                    $"Conversation '{conversation.Id}' already exists.");
            }
        }

        public async Task ReplaceAsync(
            Conversation conversation,
            CancellationToken cancellationToken = default)
        {
            var key = GetKey(conversation.Id);

            var json =
                JsonSerializer.Serialize(
                    conversation.Messages,
                    JsonOptions);

            await _database.StringSetAsync(
                key,
                json,
                TimeSpan.FromDays(30));
        }

        private static RedisKey GetKey(string id)
        {
            return $"conversation:{id}";
        }
    }
}
