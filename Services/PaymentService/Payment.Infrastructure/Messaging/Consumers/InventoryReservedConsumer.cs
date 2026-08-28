using BuildingBlocks.Contracts.IntegrationEvents.Inventory;
using MediatR;
using Payment.Application.Featurs.Payments.ProcessPayment;

namespace Payment.Infrastructure.Messaging.Consumers
{
    public sealed class InventoryReservedConsumer
    {
        private readonly ISender _sender;

        public InventoryReservedConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task ConsumeAsync(
            InventoryReservedIntegrationEvent message,
            CancellationToken cancellationToken)
        {
            var command = new ProcessPaymentCommand(
                message.OrderId);

            await _sender.Send(
                command,
                cancellationToken);
        }
    }
}
