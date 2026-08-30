using BuildingBlocks.Application.Abstractions.Persistence;
using BuildingBlocks.Application.Messaging.Inbox;
using BuildingBlocks.Contracts.Inventory;
using Order.Application.Abstractions.Persistence;

namespace Order.Application.Messaging;

public sealed class InventoryReleasedMessageHandler
    : IInventoryReleasedMessageHandler
{
    private const string ConsumerName =
        "Order.InventoryReleasedForOrderConsumer";

    private readonly IInboxRepository _inboxRepository;
    private readonly IOrderSagaRepository _sagaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InventoryReleasedMessageHandler(
        IInboxRepository inboxRepository,
        IOrderSagaRepository sagaRepository,
        IUnitOfWork unitOfWork)
    {
        _inboxRepository = inboxRepository;
        _sagaRepository = sagaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(
        InventoryReleasedForOrderIntegrationEvent message,
        CancellationToken cancellationToken)
    {
        var alreadyProcessed =
            await _inboxRepository.ExistsAsync(
                message.EventId,
                ConsumerName,
                cancellationToken);

        if (alreadyProcessed)
            return;

        var inboxMessage =
            new InboxMessage(
                message.EventId,
                ConsumerName,
                DateTime.UtcNow);

        await _inboxRepository.AddAsync(
            inboxMessage,
            cancellationToken);

        var saga =
            await _sagaRepository.GetByOrderIdAsync(
                message.OrderId,
                cancellationToken);

        if (saga is null)
        {
            throw new InvalidOperationException(
                $"Saga for Order '{message.OrderId}' was not found.");
        }

        saga.MarkInventoryReleased();

        inboxMessage.MarkAsProcessed(
            DateTime.UtcNow);

        await _unitOfWork.CommitAsync(
            cancellationToken);
    }
}