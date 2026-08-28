using BuildingBlocks.Contracts.IntegrationEvents.Inventory;
using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.Persistence;
using Inventory.Domain.Aggregates.ProductInventory.Events;

namespace Inventory.Application.DomainEventHandlers
{
    public sealed class InventoryReservedDomainEventHandler
    : IDomainEventHandler<InventoryReservedDomainEvent>
    {
        private readonly IIntegrationEventCollector _collector;

        public InventoryReservedDomainEventHandler(
            IIntegrationEventCollector collector)
        {
            _collector = collector;
        }

        public Task HandleAsync(
            InventoryReservedDomainEvent domainEvent,
            CancellationToken cancellationToken)
        {
            var integrationEvent =
                new InventoryReservedIntegrationEvent(
                    domainEvent.EventId,
                    domainEvent.OccurredOnUtc,
                    domainEvent.OrderId);

            _collector.Add(integrationEvent);

            return Task.CompletedTask;
        }
    }
}
