using BuildingBlocks.Domain.Errors;

namespace BuildingBlocks.Application
{
    public interface IUnitOfWork
    {
        //Task<Result<List<IntegrationEvents>>>   DispatchDomainEvents(IList<IDomainEvent> domainEvents);
        Task<Result> CommitAsync(CancellationToken cancellationToken);
    }
}
