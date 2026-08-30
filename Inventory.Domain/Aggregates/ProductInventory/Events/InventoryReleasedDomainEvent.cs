using BuildingBlocks.Domain.Events;

namespace Inventory.Domain.Aggregates.ProductInventory.Events
{
    public sealed record InventoryReleasedDomainEvent(
     Guid EventId,
     DateTime OccurredOnUtc,
     Guid OrderId,
     Guid ProductId,
     int Quantity)
     : IDomainEvent;
}
