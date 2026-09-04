using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Application.Messaging.Outbox;
using BuildingBlocks.Contracts.Inventory;
using Order.Application.Abstractions.Persistence;
using Order.Domain.Abnstractions;
using Order.Domain.Orders;
using System.Text.Json;

namespace Order.Application.Sagas
{
    public sealed class OrderSagaCoordinator
    {
        private readonly IOrderSagaRepository _sagaRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOutboxRepository _outboxRepository;

        public OrderSagaCoordinator(
            IOrderSagaRepository sagaRepository,
            IOrderRepository orderRepository,
            IOutboxRepository outboxRepository)
        {
            _sagaRepository = sagaRepository;
            _orderRepository = orderRepository;
            _outboxRepository = outboxRepository;
        }

        public async Task HandlePaymentFailedAsync(
            Guid orderId,
            string reason,
            CancellationToken cancellationToken)
        {
            var saga =
                await _sagaRepository.GetByOrderIdAsync(
                    orderId,
                    cancellationToken);

            if (saga is null)
            {
                throw new InvalidOperationException(
                    $"Saga for Order '{orderId}' was not found.");
            }

            saga.MarkPaymentFailed();

            var order =
                await _orderRepository.GetByIdAsync(
                    OrderId.From(orderId).Value,
                    cancellationToken);

            if (order is null)
            {
                throw new InvalidOperationException(
                    $"Order '{orderId}' was not found.");
            }

            var cancelResult = order.Cancel();

            if (!cancelResult.IsSuccess)
            {
                throw new InvalidOperationException(
                    cancelResult.Error.Code);
            }

            saga.MarkOrderCancelled();

            var message =
                new ReleaseInventoryIntegrationCommand(
                    Guid.NewGuid(),
                    orderId,
                    DateTime.UtcNow);

            var outboxMessage =
                              new OutboxMessage(
                                  Guid.NewGuid(),
                                  typeof(ReleaseInventoryIntegrationCommand).FullName!,
                                  JsonSerializer.Serialize(message),
                                  DateTime.UtcNow);

            await _outboxRepository.AddAsync(
                outboxMessage,
                cancellationToken);
        }

        public async Task HandleInventoryReleasedAsync(
    Guid orderId,
    CancellationToken cancellationToken)
        {
            var saga =
                await _sagaRepository.GetByOrderIdAsync(
                    orderId,
                    cancellationToken);

            if (saga is null)
            {
                throw new InvalidOperationException(
                    $"Saga for Order '{orderId}' was not found.");
            }

            saga.MarkInventoryReleased();
        }
    }
}