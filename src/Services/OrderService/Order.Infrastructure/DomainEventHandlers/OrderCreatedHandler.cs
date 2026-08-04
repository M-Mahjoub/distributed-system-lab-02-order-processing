using BuildingBlocks.Domain;
using Order.Domain.Orders.Events;
using Order.Infrastructure.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Infrastructure.DomainEventHandlers
{
    public sealed class OrderCreatedHandler
        : IDomainEventHandler<OrderCreatedDomainEvent>
    {
        private readonly IIntegrationEventCollector _collector;
        //private readonly IClock _clock;
        //private readonly IGuidGenerator _guidGenerator;

        public OrderCreatedHandler(
            IIntegrationEventCollector collector)
        {
            _collector = collector;
        }

        public async Task Handle(OrderCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            var integrationEvent =
               new OrderCreatedIntegrationEvent(
                   Guid.CreateVersion7(),
                   DateTime.UtcNow,
                   domainEvent.OrderId,
                   domainEvent.CustomerId);

            _collector.Add(integrationEvent);
        }
    }
}
