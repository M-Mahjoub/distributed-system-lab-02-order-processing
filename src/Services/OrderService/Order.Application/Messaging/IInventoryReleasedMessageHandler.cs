using BuildingBlocks.Contracts.Inventory;

namespace Order.Application.Messaging;

public interface IInventoryReleasedMessageHandler
{
    Task HandleAsync(
        InventoryReleasedForOrderIntegrationEvent message,
        CancellationToken cancellationToken);
}