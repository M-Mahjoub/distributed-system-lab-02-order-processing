using BuildingBlocks.Contracts.IntegrationEvents;

namespace BuildingBlocks.Application.Messaging
{
    public interface IOutboxMessageFactory
    {
        IReadOnlyCollection<OutboxMessage> Create(
        IReadOnlyCollection<IIntegrationEvent> integrationEvents);
    }
}
