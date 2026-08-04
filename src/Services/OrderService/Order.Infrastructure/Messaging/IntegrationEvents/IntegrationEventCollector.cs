using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Messaging.IntegrationEvents
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
