using ShoppingAgent.Application.AI;

namespace ShoppingAgent.Application.Agents;

public interface IAgentService
{
    Task<string> RunAsync(
        IReadOnlyList<ChatMessageDto> context,
        CancellationToken cancellationToken = default);
}