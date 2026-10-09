namespace OrderMcpServer.Contracts;

public sealed record OrderDto(
    string Id,
    string CustomerId,
    string ProductId,
    int Quantity,
    string Status);