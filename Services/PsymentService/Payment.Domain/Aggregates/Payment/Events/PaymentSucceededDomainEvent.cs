using BuildingBlocks.Domain.Events;

namespace Payment.Domain.Aggregates.Payment.Events
{
    public sealed record PaymentSucceededDomainEvent(
     Guid EventId,
     DateTime OccurredOnUtc,
     Guid OrderId)
     : IDomainEvent;
}
