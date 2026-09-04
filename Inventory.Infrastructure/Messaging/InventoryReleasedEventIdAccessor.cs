using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Contracts.Inventory;

namespace Inventory.Infrastructure.Messaging;

public sealed class InventoryReleasedEventIdAccessor
    : IMessageIdAccessor<InventoryReleasedForOrderIntegrationEvent>
{
    public Guid GetId(
        InventoryReleasedForOrderIntegrationEvent message)
    {
        return message.EventId;
    }
}