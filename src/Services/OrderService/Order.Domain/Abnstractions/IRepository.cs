namespace Order.Domain.Abnstractions
{
    public interface IRepository<TAggregate, TId> where TAggregate : BuildingBlocks.Domain.AggregateRoot<TId>
    {
        Task AddAsync(
        TAggregate aggregate,
        CancellationToken cancellationToken);
    }
}
