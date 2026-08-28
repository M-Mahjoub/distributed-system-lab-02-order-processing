namespace BuildingBlocks.Contracts.IntegrationEvents.Orders
{
    public sealed record OrderCreatedIntegrationEvent(
         Guid EventId,
         DateTime OccurredOnUtc,
         Guid OrderId,
         IReadOnlyCollection<OrderItemDto> Items) : IntegrationEvent(EventId, OccurredOnUtc)
    {
    }
}
