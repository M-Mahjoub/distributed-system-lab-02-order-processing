using BuildingBlocks.Contracts.IntegrationEvents;

namespace Order.Infrastructure.IntegrationEvents
{
    public sealed record OrderConfirmedIntegrationEvent(
         Guid EventId,
         DateTime OccurredOnUtc,
         Guid OrderId,
         Guid CustomerId) : IntegrationEvent(EventId, OccurredOnUtc)
    {
    }
}
