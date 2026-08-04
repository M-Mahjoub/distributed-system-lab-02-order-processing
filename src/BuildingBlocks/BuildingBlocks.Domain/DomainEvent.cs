using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain
{
    public class DomainEvent : IDomainEvent
    {
        public DateTime OccuredOn => throw new NotImplementedException();
    }
}
