using BuildingBlocks.Contracts.IntegrationEvents;
using Order.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Persistence.Outbox
{
    public interface IOutboxMessageFactory
    {
        IReadOnlyCollection<OutboxMessage> Create(
        IReadOnlyCollection<IIntegrationEvent> integrationEvents);
    }
}
