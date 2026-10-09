using ModelContextProtocol.Server;
using OrderMcpServer.Clients;
using OrderMcpServer.Contracts;
using System.ComponentModel;

namespace OrderMcpServer.Tools;

[McpServerToolType]
public sealed class OrderTools
{
    private readonly OrderServiceClient _client;

    public OrderTools(
        OrderServiceClient client)
    {
        _client = client;
    }

    [McpServerTool]
    [Description(
        "Creates an order for a customer.")]
    public async Task<object> CreateOrder(
        string customerId,
        string productId,
        int quantity,
        string idempotencyKey,
        CancellationToken cancellationToken = default)
    {
        if (quantity <= 0)
        {
            return new
            {
                Success = false,
                Error = "Quantity must be greater than zero."
            };
        }

        var request =
            new CreateOrderRequest(
                customerId,
                productId,
                quantity);

        var order =
            await _client.CreateOrderAsync(
                request,
                idempotencyKey,
                cancellationToken);

        return new
        {
            Success = true,
            Order = order
        };
    }
}