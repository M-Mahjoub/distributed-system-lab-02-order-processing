using BuildingBlocks.Contracts.Inventory;
using BuildingBlocks.Application.Messaging;

namespace Order.Infrastructure.Messaging.Consumers;

public sealed class InventoryReleasedForOrderConsumer
{
    private readonly ITransactionalMessageHandler<
        InventoryReleasedForOrderIntegrationEvent> _handler;

    public InventoryReleasedForOrderConsumer(
        ITransactionalMessageHandler<
            InventoryReleasedForOrderIntegrationEvent> handler)
    {
        _handler = handler;
    }

    public Task ConsumeAsync(
        InventoryReleasedForOrderIntegrationEvent message,
        CancellationToken cancellationToken)
    {
        return _handler.HandleAsync(
            message,
            cancellationToken);
    }
}