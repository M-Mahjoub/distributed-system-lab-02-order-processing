using BuildingBlocks.Contracts.IntegrationEvents.Payments;
using Inventory.Application.Features.Inventory.Release;
using MediatR;

namespace Inventory.Infrastructure.Messaging.Consumers
{
    public sealed class PaymentFailedConsumer
    {
        private readonly ISender _sender;

        public PaymentFailedConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task ConsumeAsync(
            PaymentFailedIntegrationEvent message,
            CancellationToken cancellationToken)
        {
            var command = new ReleaseInventoryCommand(
                message.OrderId);

            await _sender.Send(
                command,
                cancellationToken);
        }
    }
}
