using Order.Domain.Orders;
using Order.Domain;
using BuildingBlocks.Contracts.IntegrationEvents;
using Order.Contracts.dtos;

namespace Order.Infrastructure.IntegrationEvents
{
    public sealed record OrderCreatedIntegrationEvent(
         Guid EventId,
         DateTime OccurredOnUtc,
         OrderId OrderId,
         CustomerId CustomerId,
         IReadOnlyCollection<OrderItemDto> Items) : IntegrationEvent(EventId, OccurredOnUtc)
    {
    }
}
