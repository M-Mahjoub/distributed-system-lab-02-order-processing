using System.Net.Http.Json;
using ShoppingMcpServer.Contracts;

namespace ShoppingMcpServer.Clients;

public sealed class ProductServiceClient
{
    private readonly HttpClient _httpClient;

    public ProductServiceClient(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<ProductDto>> SearchAsync(
        string query,
        CancellationToken cancellationToken = default)
    {
        var products =
            await _httpClient.GetFromJsonAsync<
                IReadOnlyList<ProductDto>>(
                    $"/api/products/search?q={Uri.EscapeDataString(query)}",
                    cancellationToken);

        return products ?? [];
    }

    public async Task<ProductDto?> GetByIdAsync(
        string productId,
        CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<ProductDto>(
            $"/api/products/{productId}",
            cancellationToken);
    }
}