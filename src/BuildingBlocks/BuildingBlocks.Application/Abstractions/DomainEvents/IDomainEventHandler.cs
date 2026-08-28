using BuildingBlocks.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain
{
    public interface IDomainEventHandler<in TDomainEvent>
     where TDomainEvent : IDomainEvent
    {
        Task HandleAsync(
            TDomainEvent domainEvent,
            CancellationToken cancellationToken = default);
    }
}
