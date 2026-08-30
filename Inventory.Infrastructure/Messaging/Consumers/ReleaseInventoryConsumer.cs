using BuildingBlocks.Contracts.Inventory;
using Inventory.Application.Features.Inventory.Release;
using MediatR;

namespace Inventory.Infrastructure.Messaging.Consumers;

public sealed class ReleaseInventoryConsumer
{
    private readonly ISender _sender;

    public ReleaseInventoryConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task ConsumeAsync(
        ReleaseInventoryIntegrationCommand message,
        CancellationToken cancellationToken)
    {
        var command = new ReleaseInventoryCommand(
            message.OrderId);

        await _sender.Send(
            command,
            cancellationToken);
    }
}