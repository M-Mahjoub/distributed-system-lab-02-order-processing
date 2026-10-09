using ModelContextProtocol.Client;

namespace InventoryAgent.MCP;

public sealed class InventoryMcpClient
    : IAsyncDisposable
{
    private readonly McpClient _client;

    private InventoryMcpClient(
        McpClient client)
    {
        _client = client;
    }

    public static async Task<InventoryMcpClient> CreateAsync(
        string endpoint,
        CancellationToken cancellationToken = default)
    {
        var transport =
            new HttpClientTransport(
                new HttpClientTransportOptions
                {
                    Endpoint = new Uri(endpoint),
                    TransportMode =
                        HttpTransportMode.StreamableHttp
                });

        var client =
            await McpClient.CreateAsync(
                transport,
                cancellationToken: cancellationToken);

        return new InventoryMcpClient(client);
    }

    public async Task<object> CheckInventoryAsync(
        string productId,
        CancellationToken cancellationToken = default)
    {
        return await _client.CallToolAsync(
            "check_inventory",
            new Dictionary<string, object?>
            {
                ["productId"] = productId
            },
            cancellationToken: cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}