using ShoppingAgent.Application.Agents;
using ModelContextProtocol.Client;
using ShoppingAgent.Application.Abstractions;

namespace ShoppingAgent.Infrastructure.MCP;

public sealed class McpToolProvider : IToolProvider
{
    private readonly ShoppingMcpClient _mcpClient;

    public McpToolProvider(
        ShoppingMcpClient mcpClient)
    {
        _mcpClient = mcpClient;
    }

    public async Task<IReadOnlyList<IAgentTool>> GetToolsAsync(
        CancellationToken cancellationToken = default)
    {
        var tools =
            await _mcpClient.GetToolsAsync(
                cancellationToken);

        return tools
            .Select(x =>
                (IAgentTool)new McpAgentTool(x))
            .ToList();
    }
}