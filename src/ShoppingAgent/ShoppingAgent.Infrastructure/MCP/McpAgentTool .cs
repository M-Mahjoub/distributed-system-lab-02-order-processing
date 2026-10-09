using ModelContextProtocol.Client;
using ShoppingAgent.Application.Abstractions;
using ShoppingAgent.Application.Agents;

namespace ShoppingAgent.Infrastructure.MCP;

public sealed class McpAgentTool : IAgentTool
{
    public McpClientTool Tool { get; }

    public string Name =>
        Tool.Name;

    public string Description =>
        Tool.Description ?? string.Empty;

    public McpAgentTool(McpClientTool tool)
    {
        Tool = tool;
    }

    public Task<object?> ExecuteAsync(IDictionary<string, object?> arguments, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}