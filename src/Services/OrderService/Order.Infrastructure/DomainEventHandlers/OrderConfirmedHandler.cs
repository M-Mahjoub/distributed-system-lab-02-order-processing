using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.Persistence;
using Order.Domain.Orders.Events;
using Order.Infrastructure.IntegrationEvents;

namespace Order.Infrastructure.DomainEventHandlers
{
    public sealed class OrderConfirmedHandler
     : IDomainEventHandler<OrderConfirmedDomainEvent>
    {
        private readonly IIntegrationEventCollector _collector;

        public OrderConfirmedHandler(
            IIntegrationEventCollector collector)
        {
            _collector = collector;
        }

        public async Task HandleAsync(
            OrderConfirmedDomainEvent domainEvent,
            CancellationToken cancellationToken)
        {
            var integrationEvent =
                new OrderConfirmedIntegrationEvent(
                    Guid.NewGuid(),
                    domainEvent.OccurredOnUtc,
                    domainEvent.OrderId.Value,
                    domainEvent.CustomerId.Value);

            _collector.Add(integrationEvent);
        }
    }
}
