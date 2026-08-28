namespace BuildingBlocks.Contracts.IntegrationEvents.Orders
{
    public sealed record OrderItemDto(
    Guid ProductId,
    int Quantity);
}
