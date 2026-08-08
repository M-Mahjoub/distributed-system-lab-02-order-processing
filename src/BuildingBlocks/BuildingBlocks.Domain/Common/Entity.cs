namespace BuildingBlocks.Domain.Common;

public abstract class Entity<TId>
    where TId : notnull
{
    public TId Id { get; protected init; }

    protected Entity()
    {
    }

    protected Entity(TId id)
    {
        Id = id;
    }

    //چون Entity باید بر اساس Identity مقایسه شود
    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override int GetHashCode()
        => Id.GetHashCode();

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
        => Equals(left, right);

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
        => !Equals(left, right);
}
