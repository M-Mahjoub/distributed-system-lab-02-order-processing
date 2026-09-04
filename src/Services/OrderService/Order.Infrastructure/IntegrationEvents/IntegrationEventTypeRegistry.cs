using BuildingBlocks.Contracts.IntegrationEvents.Orders;
using Order.Infrastructure.Messaging.IntegrationEvents;

namespace BuildingBlocks.Contracts.IntegrationEvents
{
    public class IntegrationEventTypeRegistry : IIntegrationEventTypeRegistry
    {
        private readonly Dictionary<string, Type> _types;

        public IntegrationEventTypeRegistry()
        {
            _types = typeof(OrderCreatedIntegrationEvent)
                .Assembly
                .GetTypes()
                .Where(t => typeof(IIntegrationEvent).IsAssignableFrom(t)
                            && !t.IsInterface
                            && !t.IsAbstract)
                .ToDictionary(
                    t => t.Name,
                    t => t);
        }

        public Type Resolve(string eventType)
        {
            if (_types.TryGetValue(eventType, out var type))
                return type;

            throw new Exception(
                $"Unknown integration event: {eventType}");
        }
    }
}
