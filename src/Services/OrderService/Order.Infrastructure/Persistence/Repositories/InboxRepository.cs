using BuildingBlocks.Application.Messaging.Inbox;
using Microsoft.EntityFrameworkCore;
using Order.Application.Abstractions.Persistence;
using Order.Infrastructure.Persistence.DbContexts;

namespace Order.Infrastructure.Persistence.Repositories;

public sealed class InboxRepository : IInboxRepository
{
    private readonly OrderDbContext _dbContext;

    public InboxRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> ExistsAsync(
        Guid messageId,
        string consumer,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.InboxMessages
            .AnyAsync(
                x =>
                    x.Id == messageId &&
                    x.Consumer == consumer,
                cancellationToken);
    }

    public async Task AddAsync(
        InboxMessage message,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.InboxMessages.AddAsync(
            message,
            cancellationToken);
    }

    public Task MarkProcessedAsync(Guid messageId, DateTime processedOnUtc, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}