using BuildingBlocks.Domain.Common;

namespace Order.Domain.Abnstractions
{
    public interface IRepository<TAggregate, TId> where TAggregate : AggregateRoot<TId>
    {
        Task AddAsync(
        TAggregate aggregate,
        CancellationToken cancellationToken);
    }
}
