using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.Persistence;
using Order.Contracts.dtos;
using Order.Domain.Orders.Events;
using Order.Infrastructure.IntegrationEvents;

namespace Order.Infrastructure.DomainEventHandlers
{
    public sealed class OrderCreatedDomainEventHandler
        : IDomainEventHandler<OrderCreatedDomainEvent>
    {
        private readonly IIntegrationEventCollector _collector;
        //private readonly IClock _clock;
        //private readonly IGuidGenerator _guidGenerator;

        public OrderCreatedDomainEventHandler(
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
                   domainEvent.CustomerId,
                   new List<OrderItemDto> { new OrderItemDto(Guid.NewGuid(), 2) });

            _collector.Add(integrationEvent);
        }
    }
}
