using BuildingBlocks.Contracts.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Messaging.IntegrationEvents
{
    public interface IIntegrationEventPublisher
    {
        Task PublishAsync(
            IIntegrationEvent integrationEvent,
            CancellationToken cancellationToken = default);
    }
}
