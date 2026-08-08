using BuildingBlocks.Domain.Events;

namespace Inventory.Domain.Aggregates.Inventory.Events
{
    public sealed record InventoryReservedDomainEvent(
    InventoryId InventoryId,
    ProductId ProductId,
    int Quantity)
    : DomainEvent;
}
