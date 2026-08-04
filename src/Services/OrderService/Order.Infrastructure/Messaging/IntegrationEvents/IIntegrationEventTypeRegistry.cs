using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Messaging.IntegrationEvents
{
    public interface IIntegrationEventTypeRegistry
    {
        Type Resolve(string eventType);
    }
}
