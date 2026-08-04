using BuildingBlocks.Contracts.IntegrationEvents;
using Order.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
