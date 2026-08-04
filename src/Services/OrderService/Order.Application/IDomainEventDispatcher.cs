using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(
       IReadOnlyCollection<IDomainEvent> domainEvents,
       CancellationToken cancellationToken = default);
    }
}
