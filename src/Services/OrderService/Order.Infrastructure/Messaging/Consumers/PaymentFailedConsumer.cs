using BuildingBlocks.Contracts.IntegrationEvents.Payments;
using Order.Application.Sagas;

namespace Order.Infrastructure.Messaging.Consumers
{
    public sealed class PaymentFailedConsumer
    {
        //private readonly ISender _sender;

        private readonly OrderSagaCoordinator _coordinator;

        public PaymentFailedConsumer(
            OrderSagaCoordinator coordinator)
        {
            _coordinator = coordinator;
        }

        public async Task ConsumeAsync(
            PaymentFailedIntegrationEvent message,
            CancellationToken cancellationToken)
        {
            await _coordinator.HandlePaymentFailedAsync(
                message.OrderId,
                message.Reason,
                cancellationToken);

            //var command = new CancelOrderCommand(
            //   message.OrderId);

            //await _sender.Send(
            //    command,
            //    cancellationToken);
        }
    }
}
