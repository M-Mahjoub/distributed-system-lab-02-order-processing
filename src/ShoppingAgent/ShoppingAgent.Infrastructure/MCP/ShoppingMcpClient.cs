using ModelContextProtocol.Client;

namespace ShoppingAgent.Infrastructure.MCP;

public sealed class ShoppingMcpClient
    : IMcpToolProvider, IAsyncDisposable
{
    private readonly McpClient _client;

    private ShoppingMcpClient(McpClient client)
    {
        _client = client;
    }

    public static async Task<ShoppingMcpClient> CreateAsync(
        string endpoint,
        CancellationToken cancellationToken = default)
    {
        var transport =
            new HttpClientTransport(
                new HttpClientTransportOptions
                {
                    Endpoint = new Uri(endpoint),

                    TransportMode =
                        HttpTransportMode.StreamableHttp,

                    ConnectionTimeout =
                        TimeSpan.FromSeconds(30)
                });

        var client =
            await McpClient.CreateAsync(
                transport,
                cancellationToken: cancellationToken);

        return new ShoppingMcpClient(client);
    }

    public async ValueTask<IList<McpClientTool>> GetToolsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _client.ListToolsAsync(
            cancellationToken: cancellationToken);
    }

    public async Task<object> SearchProductsAsync(
        string query,
        CancellationToken cancellationToken = default)
    {
        return await _client.CallToolAsync(
            "search_products",
            new Dictionary<string, object?>
            {
                ["query"] = query
            },
            cancellationToken: cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}