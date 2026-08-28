namespace BuildingBlocks.Domain.Events;

public interface IDomainEvent
{
    DateTime OccurredOnUtc { get; }
}
