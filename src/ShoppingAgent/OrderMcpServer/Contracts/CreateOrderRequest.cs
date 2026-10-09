namespace OrderMcpServer.Contracts;

public sealed record CreateOrderRequest(
    string CustomerId,
    string ProductId,
    int Quantity);