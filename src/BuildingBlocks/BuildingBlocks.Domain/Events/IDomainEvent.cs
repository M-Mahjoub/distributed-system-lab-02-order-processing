namespace BuildingBlocks.Domain.Events;

public interface IDomainEvent
{
    DateTime OccuredOn { get; }
}
