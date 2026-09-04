using BuildingBlocks.Application.Abstractions.DomainEvents;
using BuildingBlocks.Application.Abstractions.Persistence;
using BuildingBlocks.Application.Messaging.Outbox;
using BuildingBlocks.Domain.Errors;
using BuildingBlocks.Infrastructure.Persistence.Extensions;

namespace BuildingBlocks.Infrastructure.Persistence
{
    public  class EfUnitOfWork<TDbContext> : IUnitOfWork where TDbContext : ApplicationDbContext
    {
        private readonly TDbContext  _dbContext;

        private readonly IDomainEventDispatcher _dispatcher;

        private readonly IIntegrationEventCollector _collector;

        private readonly IOutboxMessageFactory _factory;

        public EfUnitOfWork(
            TDbContext dbContext,
            IDomainEventDispatcher dispatcher,
            IIntegrationEventCollector collector,
            IOutboxMessageFactory factory)
        {
            _dbContext = dbContext;
            _dispatcher = dispatcher;
            _collector = collector;
            _factory = factory;
        }
        public async Task<Result> CommitAsync(CancellationToken cancellationToken)
        {
            await using var transaction =
                            await _dbContext.Database.BeginTransactionAsync(
                                cancellationToken);

            try
            {
                await DispatchDomainEventsAsync(cancellationToken);

                AddOutboxMessages();

                await _dbContext.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }

            return Result.Success();
        }

        private void AddOutboxMessages()
        {
            var events = _collector.Dequeue();
            var outboxMessages = _factory.Create(events);
            _dbContext.Set<OutboxMessage>()
                      .AddRange(outboxMessages);
        }

        private async Task DispatchDomainEventsAsync(
                           CancellationToken cancellationToken)
        {
            while (true)
            {

                var aggregates = _dbContext.GetAggregatesWithEvents();
                var domainEvents = _dbContext.GetDomainEvents();


                if (domainEvents.Count == 0)
                    break;

                foreach (var aggregate in aggregates)
                {
                    aggregate.ClearDomainEvents();
                }

                await _dispatcher.DispatchAsync(
                    domainEvents,
                    cancellationToken);
            }
        }

    }
}
