using BuildingBlocks.Contracts.IntegrationEvents;

namespace BuildingBlocks.Infrastructure.Persistence.Outbox
{
    public interface IOutboxMessageFactory
    {
        IReadOnlyCollection<OutboxMessage> Create(
        IReadOnlyCollection<IIntegrationEvent> integrationEvents);
    }
}
