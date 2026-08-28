using ShoppingAgent.Application.AI;
using System.Text.Json;
using ShoppingAgent.Application.Abstractions;
using StackExchange.Redis;

namespace ShoppingAgent.Infrastructure.Repositories
{
    //ذخیره و بازیابی
    public sealed class RedisConversationRepository
        : IConversationRepository
    {
        private readonly IDatabase _database;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
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
            var metadataKey = GetMetadataKey(id);
            var messagesKey = GetMessagesKey(id);

            var metadataValue =
                await _database.StringGetAsync(metadataKey);

            if (metadataValue.IsNullOrEmpty)
            {
                return null;
            }

            var metadata =
                JsonSerializer.Deserialize<ConversationMetadata>(
                    metadataValue!,
                    JsonOptions);

            if (metadata is null)
            {
                return null;
            }

            var values =
                await _database.ListRangeAsync(messagesKey);

            var messages = new List<ChatMessageDto>();

            foreach (var value in values)
            {
                var message =
                    JsonSerializer.Deserialize<ChatMessageDto>(
                        value!,
                        JsonOptions);

                if (message is not null)
                {
                    messages.Add(message);
                }
            }

            return new Conversation(
                id,
                messages,
                metadata.Summary);
        }

        public async Task CreateAsync(
            Conversation conversation,
            CancellationToken cancellationToken = default)
        {
            var metadataKey =
                GetMetadataKey(conversation.Id);

            var messagesKey =
                GetMessagesKey(conversation.Id);

            var metadata = new ConversationMetadata
            {
                Summary = conversation.Summary,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            var metadataJson =
                JsonSerializer.Serialize(
                    metadata,
                    JsonOptions);

            await _database.StringSetAsync(
                metadataKey,
                metadataJson,
                TimeSpan.FromDays(30),
                When.NotExists);

            foreach (var message in conversation.Messages)
            {
                var messageJson =
                    JsonSerializer.Serialize(
                        message,
                        JsonOptions);

                await _database.ListRightPushAsync(
                    messagesKey,
                    messageJson);
            }

            await _database.KeyExpireAsync(
                messagesKey,
                TimeSpan.FromDays(30));
        }

        public async Task<bool> AppendMessageAsync(
    string conversationId,
    ChatMessageDto message,
    CancellationToken cancellationToken = default)
        {
            var messagesKey =
                GetMessagesKey(conversationId);

            var messageIdsKey =
                GetMessageIdsKey(conversationId);

            var added =
                await _database.SetAddAsync(
                    messageIdsKey,
                    message.Id.ToString());

            if (!added)
            {
                // قبلاً ذخیره شده
                return false;
            }

            var json =
                JsonSerializer.Serialize(
                    message,
                    JsonOptions);

            await _database.ListRightPushAsync(
                messagesKey,
                json);

            await UpdateUpdatedAtAsync(
                conversationId);

            return true;
        }

        public async Task UpdateSummaryAsync(
            string conversationId,
            string summary,
            CancellationToken cancellationToken = default)
        {
            var key =
                GetMetadataKey(conversationId);

            var value =
                await _database.StringGetAsync(key);

            if (value.IsNullOrEmpty)
            {
                return;
            }

            var metadata =
                JsonSerializer.Deserialize<ConversationMetadata>(
                    value!,
                    JsonOptions);

            if (metadata is null)
            {
                return;
            }

            metadata.Summary = summary;
            metadata.UpdatedAt = DateTimeOffset.UtcNow;

            var json =
                JsonSerializer.Serialize(
                    metadata,
                    JsonOptions);

            await _database.StringSetAsync(
                key,
                json,
                TimeSpan.FromDays(30));
        }

        private async Task UpdateUpdatedAtAsync(
            string conversationId)
        {
            var key =
                GetMetadataKey(conversationId);

            var value =
                await _database.StringGetAsync(key);

            if (value.IsNullOrEmpty)
            {
                return;
            }

            var metadata =
                JsonSerializer.Deserialize<ConversationMetadata>(
                    value!,
                    JsonOptions);

            if (metadata is null)
            {
                return;
            }

            metadata.UpdatedAt =
                DateTimeOffset.UtcNow;

            var json =
                JsonSerializer.Serialize(
                    metadata,
                    JsonOptions);

            await _database.StringSetAsync(
                key,
                json,
                TimeSpan.FromDays(30));
        }

        private static RedisKey GetMessagesKey(string id)
        {
            return $"conversation:{id}:messages";
        }

        private static RedisKey GetMetadataKey(string id)
        {
            return $"conversation:{id}:metadata";
        }

        private static RedisKey GetMessageIdsKey(string id)
        {
            return $"conversation:{id}:message-ids";
        }
    }
}
