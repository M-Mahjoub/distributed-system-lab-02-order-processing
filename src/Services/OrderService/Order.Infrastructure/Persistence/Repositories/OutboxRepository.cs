using BuildingBlocks.Application.Messaging.Outbox;
using Order.Infrastructure.Persistence.DbContexts;

namespace Order.Infrastructure.Persistence.Repositories;

public sealed class OutboxRepository
    : IOutboxRepository
{
    private readonly OrderDbContext _dbContext;

    public OutboxRepository(
        OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        OutboxMessage message,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.OutboxMessages.AddAsync(
            message,
            cancellationToken);
    }
}