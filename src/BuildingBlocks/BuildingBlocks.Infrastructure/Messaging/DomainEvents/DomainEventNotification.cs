using BuildingBlocks.Domain.Events;
using MediatR;

namespace BuildingBlocks.Infrastructure.Messaging.DomainEvents
{
    internal sealed record DomainEventNotification(
     IDomainEvent DomainEvent) : INotification;
}
