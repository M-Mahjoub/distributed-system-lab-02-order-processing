using BuildingBlocks.Domain.Events;

namespace BuildingBlocks.Application.Abstractions.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(
       IReadOnlyCollection<IDomainEvent> domainEvents,
       CancellationToken cancellationToken = default);
    }
}
