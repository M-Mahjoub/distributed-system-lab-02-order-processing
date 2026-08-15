using ShoppingAgent.Application.Abstractions;

namespace ShoppingAgent.Infrastructure.Tools
{
    public sealed class GetCurrentTimeTool : IAgentTool
    {
        public string Name => "get_current_time";

        public string Description =>
            "Returns the current UTC date and time.";

        public Task<object?> ExecuteAsync(
            IDictionary<string, object?> arguments,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult<object?>(
                DateTimeOffset.UtcNow);
        }
    }
}
