namespace BuildingBlocks.Domain;

public interface IDomainEvent
{
    DateTime OccuredOn { get; }
}
