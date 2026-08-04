using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure
{
    //چون Outbox به این سه مقدار احتیاج دارد.
    public interface IIntegrationEvent
    {
        Guid Id { get; }

        DateTime OccurredOnUtc { get; }

        int Version { get; }
    }
}
