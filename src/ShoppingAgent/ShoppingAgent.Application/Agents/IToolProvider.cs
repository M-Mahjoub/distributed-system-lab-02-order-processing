using ShoppingAgent.Application.Abstractions;

namespace ShoppingAgent.Application.Agents;

public interface IToolProvider
{
    Task<IReadOnlyList<IAgentTool>> GetToolsAsync(
        CancellationToken cancellationToken = default);
}