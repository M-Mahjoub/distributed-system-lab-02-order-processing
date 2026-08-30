using BuildingBlocks.Contracts.Inventory;
using Order.Application.Messaging;

namespace Order.Infrastructure.Messaging.Consumers;

public sealed class InventoryReleasedForOrderConsumer
{
    private readonly IInventoryReleasedMessageHandler _handler;

    public InventoryReleasedForOrderConsumer(
        IInventoryReleasedMessageHandler handler)
    {
        _handler = handler;
    }

    public async Task ConsumeAsync(
        InventoryReleasedForOrderIntegrationEvent message,
        CancellationToken cancellationToken)
    {
        await _handler.HandleAsync(
            message,
            cancellationToken);
    }
}