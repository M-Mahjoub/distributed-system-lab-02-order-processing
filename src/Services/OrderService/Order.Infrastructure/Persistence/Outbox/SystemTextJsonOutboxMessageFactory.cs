using BuildingBlocks.Contracts.IntegrationEvents;
using BuildingBlocks.Infrastructure.Persistence.Outbox;
using System.Text.Json;

namespace Order.Infrastructure.Persistence.Outbox
{
    public sealed class SystemTextJsonOutboxMessageFactory
     : IOutboxMessageFactory
    {
        private readonly JsonSerializerOptions _options;

        public SystemTextJsonOutboxMessageFactory(
            JsonSerializerOptions options)
        {
            _options = options;
        }

        public IReadOnlyCollection<OutboxMessage> Create(
            IReadOnlyCollection<IIntegrationEvent> integrationEvents)
        {
            return integrationEvents
                .Select(Create)
                .ToList();
        }

        private OutboxMessage Create(
            IIntegrationEvent integrationEvent)
        {
            var payload =
                JsonSerializer.Serialize(
                    integrationEvent,
                    integrationEvent.GetType(),
                    _options);

            return new OutboxMessage(
                Guid.CreateVersion7(),
                integrationEvent.GetType().Name!,
                payload,
                DateTime.UtcNow);
        }
    }
}
