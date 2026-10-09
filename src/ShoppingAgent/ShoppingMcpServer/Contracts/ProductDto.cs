namespace ShoppingMcpServer.Contracts;

public sealed record ProductDto(
    string Id,
    string Name,
    string Category,
    decimal Price,
    string Currency);