namespace ShoppingAgent.Infrastructure.Memory;

using System.Text.Json;
using ShoppingAgent.Application.Memory;
using StackExchange.Redis;

public sealed class RedisLongTermMemory
    : ILongTermMemory
{
    private readonly IDatabase _redis;

    private static readonly JsonSerializerOptions JsonOptions =
        new(JsonSerializerDefaults.Web);

    public RedisLongTermMemory(
        IConnectionMultiplexer redis)
    {
        _redis = redis.GetDatabase();
    }

    public async Task SaveAsync(
        string userId,
        MemoryItem item,
        CancellationToken cancellationToken = default)
    {
        var key = GetKey(item.UserId);

        var json =
            JsonSerializer.Serialize(
                item,
                JsonOptions);

        await _redis.ListRightPushAsync(
            key,
            json);
    }

    public async Task<IReadOnlyList<MemoryItem>> SearchAsync(
        string userId,
        string query,
        CancellationToken cancellationToken = default)
    {
        var key = GetKey(userId);

        var values =
            await _redis.ListRangeAsync(key);

        var memories =
            values
                .Select(x =>
                    JsonSerializer.Deserialize<MemoryItem>(
                        x!,
                        JsonOptions))
                .Where(x => x is not null)
                .Cast<MemoryItem>()
                .ToList();

        if (string.IsNullOrWhiteSpace(query))
            return memories;

        return memories
            .Where(x =>
                x.Content.Contains(
                    query,
                    StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    private static RedisKey GetKey(
        string userId)
    {
        return $"user:{userId}:memories";
    }
}