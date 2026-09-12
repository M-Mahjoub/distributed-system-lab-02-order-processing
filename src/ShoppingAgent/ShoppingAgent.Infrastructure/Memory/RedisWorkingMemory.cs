using System.Text.Json;
using StackExchange.Redis;
using ShoppingAgent.Application.Memory;

namespace ShoppingAgent.Infrastructure.Memory;

public sealed class RedisWorkingMemory : IWorkingMemory
{
    private readonly IDatabase _redis;

    private static readonly JsonSerializerOptions JsonOptions =
        new(JsonSerializerDefaults.Web);

    public RedisWorkingMemory(
        IConnectionMultiplexer redis)
    {
        _redis = redis.GetDatabase();
    }

    public async Task<WorkingMemory?> GetAsync(
        string conversationId,
        CancellationToken cancellationToken = default)
    {
        var key = GetKey(conversationId);

        var value = await _redis.StringGetAsync(key);

        if (value.IsNullOrEmpty)
            return null;

        return JsonSerializer.Deserialize<WorkingMemory>(
            value.ToString(),
            JsonOptions);
    }

    public async Task SaveAsync(
        string conversationId,
        WorkingMemory memory,
        CancellationToken cancellationToken = default)
    {
        var key = GetKey(conversationId);

        var json = JsonSerializer.Serialize(
            memory,
            JsonOptions);

        await _redis.StringSetAsync(
            key,
            json,
            TimeSpan.FromDays(30));
    }

    private static RedisKey GetKey(
        string conversationId)
    {
        return $"conversation:{conversationId}:working-memory";
    }
}