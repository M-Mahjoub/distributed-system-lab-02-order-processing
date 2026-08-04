using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure
{
    public abstract record IntegrationEvent : IIntegrationEvent
    {
        public Guid Id { get; init; } = Guid.CreateVersion7();

        public DateTime OccurredOnUtc { get; init; }
            = DateTime.UtcNow;

        public int Version { get; init; } = 1;
    }
}
