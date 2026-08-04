using BuildingBlocks.Application;
using BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Order.Application;
using Order.Infrastructure.Persistence.DbContexts;
using Order.Infrastructure.Persistence.Extensions;
using Order.Infrastructure.Persistence.Outbox;

namespace Order.Infrastructure.Persistence
{
    public class EfUnitOfWork : IUnitOfWork
    {
        //private IDbContextTransaction _transaction;

        private readonly OrderDbContext _dbContext;
        public IDomainEventDispatcher _domainEventDispatcher { get; set; }
        public IOutboxMessageFactory _outboxMessageFactory { get; set; }
        public IIntegrationEventCollector _integrationEventCollector { get; set; }

        public EfUnitOfWork(IDomainEventDispatcher domainEventDispatcher,
                          IIntegrationEventCollector integrationEventCollector,
                          IOutboxMessageFactory outboxMessageFactory,
                          OrderDbContext dbContext)
        {
            _integrationEventCollector = integrationEventCollector;
            _domainEventDispatcher = domainEventDispatcher;
            _outboxMessageFactory = outboxMessageFactory;
            _dbContext = dbContext;
        }
        public Task BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Result> CommitAsync(CancellationToken cancellationToken)
        {
            //اکر TimeOut شود Execution Strategy دوباره Transaction را اجرا می‌کند.
            var strategy = _dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction =
                              await _dbContext.Database
                                    .BeginTransactionAsync(cancellationToken);

                var domainEvents = _dbContext.GetDomainEvents();

                //3.DomainEventHandlerها را اجرا کن
                await _domainEventDispatcher.DispatchAsync(domainEvents);

                //4.IntegrationEventها را تولید کن
                var integerationEvents = _integrationEventCollector.Dequeue();

                // 5.IntegrationEventها را داخل Outbox ذخیره کن
                var outboxMessages = _outboxMessageFactory.Create(integerationEvents);
                _dbContext.OutboxMessages.AddRange(outboxMessages);

                // 6.SaveChanges()
                await _dbContext.SaveChangesAsync(cancellationToken);
                //7.Commit Transaction
                await transaction.CommitAsync(cancellationToken);

                // 8.پاک کردن DomainEventها
                _dbContext.ClearDomainEvents();
            });

            return Result.Success();
        }

        public Task RollbackAsync()
        {
            throw new NotImplementedException();
        }
    }

    //public sealed class EfUnitOfWork<TContext>
    //: IUnitOfWork
    //where TContext : ApplicationDbContext
    //{
    //    private readonly TContext _context;

    //    private readonly IDomainEventDispatcher _dispatcher;

    //    private readonly IIntegrationEventCollector _collector;

    //    public EfUnitOfWork(
    //        TContext context,
    //        IDomainEventDispatcher dispatcher,
    //        IIntegrationEventCollector collector)
    //    {
    //        _context = context;
    //        _dispatcher = dispatcher;
    //        _collector = collector;
    //    }

    //    public async Task<Result> CommitAsync(
    //        CancellationToken cancellationToken)
    //    {
    //    }
    //}
}
