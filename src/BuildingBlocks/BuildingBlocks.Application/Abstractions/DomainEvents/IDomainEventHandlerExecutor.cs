using BuildingBlocks.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure
{
    public interface IDomainEventHandlerExecutor
    {
        Task Execute(
        IDomainEvent domainEvent,
        CancellationToken cancellationToken);
    }
}
