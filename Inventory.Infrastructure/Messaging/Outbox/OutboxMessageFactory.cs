using System.Text.Json;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Contracts.IntegrationEvents;

namespace Inventory.Infrastructure.Messaging.Outbox;

public sealed class OutboxMessageFactory
    : IOutboxMessageFactory
{
    public OutboxMessage Create<TMessage>(
        TMessage message)
    {
        return new OutboxMessage(
            Guid.NewGuid(),
            typeof(TMessage).FullName!,
            JsonSerializer.Serialize(message),
            DateTime.UtcNow);
    }

    public IReadOnlyCollection<OutboxMessage> Create(IReadOnlyCollection<IIntegrationEvent> integrationEvents)
    {
        throw new NotImplementedException();
    }
}