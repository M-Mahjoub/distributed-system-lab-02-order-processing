using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain.Events
{
    public record DomainEvent : IDomainEvent
    {
        public DateTime OccurredOnUtc => throw new NotImplementedException();
    }
}
