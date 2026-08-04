using BuildingBlocks.Domain;
using Order.Domain.Orders.Events;
using Order.Infrastructure.IntegrationEvents;

namespace Order.Infrastructure.Handlers
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

        public async Task Handle(
            OrderConfirmedDomainEvent domainEvent,
            CancellationToken cancellationToken)
        {
            var integrationEvent =
                new OrderConfirmedIntegrationEvent(
                    domainEvent.OrderId,
                    domainEvent.CustomerId);

            _collector.Add(integrationEvent);
        }
    }
}
