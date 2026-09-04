using BuildingBlocks.Contracts.Inventory;
using Order.Application.Abstractions.Persistence;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Contracts.IntegrationEvents.Payments;
using BuildingBlocks.Contracts.IntegrationEvents;
using BuildingBlocks.Application.Messaging.Outbox;

namespace Order.Application.Messaging;

public sealed class PaymentFailedMessageHandler
    : IMessageHandler<PaymentFailedIntegrationEvent>
{
    private readonly IOrderSagaRepository _sagaRepository;
    private readonly IOutboxRepository _outboxRepository;
    private readonly IOutboxMessageFactory _outboxMessageFactory;

    public PaymentFailedMessageHandler(
        IOrderSagaRepository sagaRepository,
        IOutboxRepository outboxRepository,
        IOutboxMessageFactory outboxMessageFactory)
    {
        _sagaRepository = sagaRepository;
        _outboxRepository = outboxRepository;
        _outboxMessageFactory = outboxMessageFactory;
    }

    public async Task HandleAsync(
        PaymentFailedIntegrationEvent message,
        CancellationToken cancellationToken)
    {
        var saga =
            await _sagaRepository.GetByOrderIdAsync(
                message.OrderId,
                cancellationToken);

        if (saga is null)
        {
            throw new InvalidOperationException(
                $"Saga for order '{message.OrderId}' was not found.");
        }

        saga.MarkPaymentFailed();

        var cancelOrderCommand =
            new CancelOrderIntegrationCommand(
                Guid.NewGuid(),
                message.OrderId,
                DateTime.UtcNow);

        var releaseInventoryCommand =
            new ReleaseInventoryIntegrationCommand(
                Guid.NewGuid(),
                message.OrderId,
                DateTime.UtcNow);

        var cancelOrderOutbox =
            _outboxMessageFactory.Create(
                new List<IntegrationEvent> { cancelOrderCommand });

        var releaseInventoryOutbox =
            _outboxMessageFactory.Create(
             new List<IntegrationEvent> { releaseInventoryCommand });

        await _outboxRepository.AddAsync(
            cancelOrderOutbox.First(),
            cancellationToken);

        await _outboxRepository.AddAsync(
            releaseInventoryOutbox.First(),
            cancellationToken);
    }
}