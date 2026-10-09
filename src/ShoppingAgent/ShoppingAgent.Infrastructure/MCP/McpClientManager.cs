using ModelContextProtocol.Client;

namespace ShoppingAgent.Infrastructure.MCP;

public sealed class McpClientManager
    : IAsyncDisposable
{
    private readonly Dictionary<
        string,
        McpClient> _clients = [];

    public async Task ConnectAsync(
        McpServerConnection server,
        CancellationToken cancellationToken = default)
    {
        if (_clients.ContainsKey(server.Name))
            return;

        var transport =
            new HttpClientTransport(
                new HttpClientTransportOptions
                {
                    Endpoint = new Uri(server.Endpoint),
                    TransportMode =
                        HttpTransportMode.StreamableHttp
                });

        var client =
            await McpClient.CreateAsync(
                transport,
                cancellationToken: cancellationToken);

        _clients.Add(
            server.Name,
            client);
    }

    public McpClient GetClient(
        string serverName)
    {
        if (!_clients.TryGetValue(
                serverName,
                out var client))
        {
            throw new InvalidOperationException(
                $"MCP server '{serverName}' is not connected.");
        }

        return client;
    }

    public async Task<IReadOnlyList<McpClientTool>>
        GetAllToolsAsync(
            CancellationToken cancellationToken = default)
    {
        var result =
            new List<McpClientTool>();

        foreach (var client in _clients.Values)
        {
            var tools =
                await client.ListToolsAsync(
                    cancellationToken: cancellationToken);

            result.AddRange(tools);
        }

        return result;
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var client in _clients.Values)
        {
            await client.DisposeAsync();
        }

        _clients.Clear();
    }
}