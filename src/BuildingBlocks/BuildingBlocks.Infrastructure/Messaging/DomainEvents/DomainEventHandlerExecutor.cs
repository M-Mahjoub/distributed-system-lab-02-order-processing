using BuildingBlocks.Domain;
using BuildingBlocks.Domain.Events;
using Order.Infrastructure;

namespace BuildingBlocks.Infrastructure.Messaging.DomainEvents
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
            return _handler.HandleAsync(
                (TDomainEvent)domainEvent,
                cancellationToken);
        }
    }
}
