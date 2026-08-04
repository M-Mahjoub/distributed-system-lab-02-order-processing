using BuildingBlocks.Contracts.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure
{
    public interface IIntegrationEventCollector
    {
        void Add(IIntegrationEvent integrationEvent);

        IReadOnlyCollection<IIntegrationEvent> Dequeue();
    }
}
