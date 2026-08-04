using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Messaging.DomainEvents
{
    public sealed class DomainEventHandlerExecutor<TDomainEvent>
      : IDomainEventHandlerExecutor
      where TDomainEvent : IDomainEvent
    {
        private readonly IDomainEventHandler<TDomainEvent> _handler;

        public DomainEventHandlerExecutor(
            IDomainEventHandler<TDomainEvent> handler)
        {
            _handler = handler;
        }

        public Task Execute(
            IDomainEvent domainEvent,
            CancellationToken cancellationToken)
        {
            return _handler.Handle(
                (TDomainEvent)domainEvent,
                cancellationToken);
        }
    }
}
