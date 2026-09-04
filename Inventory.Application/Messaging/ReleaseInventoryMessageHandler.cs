using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Application.Messaging.Outbox;
using BuildingBlocks.Contracts.IntegrationEvents;
using BuildingBlocks.Contracts.Inventory;
using Inventory.Application.Abstractions.Persistence;

namespace Inventory.Application.Messaging;

public sealed class ReleaseInventoryMessageHandler
    : IMessageHandler<ReleaseInventoryIntegrationCommand>
{
    private readonly IProductInventoryRepository _repository;
    private readonly IOutboxRepository _outboxRepository;
    private readonly IOutboxMessageFactory _outboxMessageFactory;

    public ReleaseInventoryMessageHandler(
        IProductInventoryRepository repository,
        IOutboxRepository outboxRepository,
        IOutboxMessageFactory outboxMessageFactory)
    {
        _repository = repository;
        _outboxRepository = outboxRepository;
        _outboxMessageFactory = outboxMessageFactory;
    }

    public async Task HandleAsync(
        ReleaseInventoryIntegrationCommand message,
        CancellationToken cancellationToken)
    {
        var inventories =
            await _repository.GetByOrderIdAsync(
                message.OrderId,
                cancellationToken);

        if (inventories.Count == 0)
        {
            throw new InvalidOperationException(
                $"No inventory reservation found for order {message.OrderId}.");
        }

        foreach (var inventory in inventories)
        {
            var result =
                inventory.Release(message.OrderId);

            if (!result.IsSuccess)
            {
                throw new InvalidOperationException(
                    result.Error.Code);
            }
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
    }
}