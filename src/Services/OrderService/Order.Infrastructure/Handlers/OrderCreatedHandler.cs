using BuildingBlocks.Domain;
using Order.Domain.Orders.Events;
using Order.Infrastructure.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Handlers
{
    public sealed class OrderCreatedHandler 
        : IDomainEventHandler<OrderCreatedDomainEvent>
    {
        private readonly IIntegrationEventCollector _collector;

        public OrderCreatedHandler(
            IIntegrationEventCollector collector)
        {
            _collector = collector;
        }

        public async Task Handle(OrderCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            var integrationEvent =
               new OrderCreatedIntegrationEvent(
                   domainEvent.OrderId,
                   domainEvent.CustomerId);

            _collector.Add(integrationEvent);
        }
    }
}
