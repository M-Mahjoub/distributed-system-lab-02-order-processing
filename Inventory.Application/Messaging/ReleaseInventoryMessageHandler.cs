using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Application.Messaging.Inbox;
using BuildingBlocks.Contracts.IntegrationEvents;
using BuildingBlocks.Contracts.Inventory;
using Inventory.Application.Abstractions.Persistence;
using Inventory.Application.Features.Inventory.Release;

namespace Inventory.Application.Messaging;

public sealed class ReleaseInventoryMessageHandler
    : IMessageHandler<ReleaseInventoryIntegrationCommand>
{
    private const string ConsumerName =
        "Inventory.ReleaseInventoryConsumer";

    private readonly IInboxRepository _inboxRepository;
    private readonly IProductInventoryRepository _inventoryRepository;
    private readonly IOutboxRepository _outboxRepository;
    private readonly IOutboxMessageFactory _outboxMessageFactory;
    private readonly BuildingBlocks.Application.Abstractions.Persistence.IUnitOfWork _unitOfWork;

    public ReleaseInventoryMessageHandler(
        IInboxRepository inboxRepository,
        IProductInventoryRepository inventoryRepository,
        IOutboxRepository outboxRepository,
        IOutboxMessageFactory outboxMessageFactory,
        BuildingBlocks.Application.Abstractions.Persistence.IUnitOfWork unitOfWork)
    {
        _inboxRepository = inboxRepository;
        _inventoryRepository = inventoryRepository;
        _outboxRepository = outboxRepository;
        _outboxMessageFactory = outboxMessageFactory;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(
        ReleaseInventoryIntegrationCommand message,
        CancellationToken cancellationToken)
    {
        var alreadyProcessed =
            await _inboxRepository.ExistsAsync(
                message.MessageId,
                ConsumerName,
                cancellationToken);

        if (alreadyProcessed)
            return;

        var inboxMessage =
            new InboxMessage(
                message.MessageId,
                ConsumerName,
                DateTime.UtcNow);

        await _inboxRepository.AddAsync(
            inboxMessage,
            cancellationToken);

        var inventories =
            await _inventoryRepository.GetByOrderIdAsync(
                message.OrderId,
                cancellationToken);

        foreach (var inventory in inventories)
        {
            var result =
                inventory.Release(message.OrderId);

            if (!result.IsSuccess)
                throw new InvalidOperationException(
                    result.Error.Code);
        }

        var integrationEvent =
            new InventoryReleasedForOrderIntegrationEvent(
                Guid.NewGuid(),
                message.OrderId,
                DateTime.UtcNow);

        var outboxMessage =
            _outboxMessageFactory.Create(
             new List<IntegrationEvent> { integrationEvent });

        await _outboxRepository.AddAsync(
            outboxMessage.First(),
            cancellationToken);

        inboxMessage.MarkAsProcessed(
            DateTime.UtcNow);

        await _unitOfWork.CommitAsync(
            cancellationToken);
    }
}