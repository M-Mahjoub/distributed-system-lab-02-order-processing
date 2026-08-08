using BuildingBlocks.Contracts.IntegrationEvents;
using BuildingBlocks.Infrastructure.Persistence;

namespace BuildingBlocks.Infrastructure.Messaging.IntegrationEvents
{
    public sealed class IntegrationEventCollector
     : IIntegrationEventCollector
    {
        private readonly List<IIntegrationEvent> _events = [];

        public void Add(IIntegrationEvent integrationEvent)
        {
            _events.Add(integrationEvent);
        }

        public IReadOnlyCollection<IIntegrationEvent> Dequeue()
        {
            var events = _events.ToList();

            _events.Clear();

            return events;
        }
    }
}
