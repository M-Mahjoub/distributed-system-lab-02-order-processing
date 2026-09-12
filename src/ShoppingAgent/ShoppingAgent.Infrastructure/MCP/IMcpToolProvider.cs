using ModelContextProtocol.Client;

public interface IMcpToolProvider
{
    ValueTask<IList<McpClientTool>> GetToolsAsync(
        CancellationToken cancellationToken = default);
}