using Order.Domain.Orders;
using Order.Domain;
using BuildingBlocks.Contracts.IntegrationEvents;

namespace Order.Infrastructure.IntegrationEvents
{
    public sealed record OrderConfirmedIntegrationEvent(
         Guid EventId,
         DateTime OccurredOnUtc,
         OrderId OrderId,
         CustomerId CustomerId) : IntegrationEvent(EventId, OccurredOnUtc)
    {
    }
}
