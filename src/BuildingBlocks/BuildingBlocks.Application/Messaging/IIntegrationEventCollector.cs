using BuildingBlocks.Contracts.IntegrationEvents;

namespace BuildingBlocks.Infrastructure.Persistence
{
    public interface IIntegrationEventCollector
    {
        void Add(IIntegrationEvent integrationEvent);

        IReadOnlyCollection<IIntegrationEvent> Dequeue();
    }
}
