using BuildingBlocks.Domain;

namespace BuildingBlocks.Application
{
    public interface IUnitOfWork
    {
        //Task<Result<List<IntegrationEvents>>>   DispatchDomainEvents(IList<IDomainEvent> domainEvents);
        Task BeginTransactionAsync();
        Task RollbackAsync();
        Task<Result> CommitAsync(CancellationToken cancellationToken);
    }
}
