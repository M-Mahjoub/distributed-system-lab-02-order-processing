using BuildingBlocks.Contracts.Inventory;

namespace Inventory.Application.Messaging;

public interface IReleaseInventoryMessageHandler
{
    Task HandleAsync(
        ReleaseInventoryIntegrationCommand message,
        CancellationToken cancellationToken);
}