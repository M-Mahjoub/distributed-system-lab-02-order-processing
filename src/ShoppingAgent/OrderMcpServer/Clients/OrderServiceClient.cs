using System.Net.Http.Json;
using OrderMcpServer.Contracts;

namespace OrderMcpServer.Clients;

public sealed class OrderServiceClient
{
    private readonly HttpClient _httpClient;

    public OrderServiceClient(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<OrderDto?> CreateOrderAsync(
        CreateOrderRequest request,
        string idempotencyKey,
        CancellationToken cancellationToken = default)
    {
        using var httpRequest =
            new HttpRequestMessage(
                HttpMethod.Post,
                "/api/orders");

        httpRequest.Headers.Add(
            "Idempotency-Key",
            idempotencyKey);

        httpRequest.Content =
            JsonContent.Create(request);

        using var response =
            await _httpClient.SendAsync(
                httpRequest,
                cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<OrderDto>(
                cancellationToken);
    }
}