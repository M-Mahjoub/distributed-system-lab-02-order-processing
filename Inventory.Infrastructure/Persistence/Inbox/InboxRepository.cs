using BuildingBlocks.Application.Messaging.Inbox;
using Inventory.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.Inbox;

public sealed class InboxRepository : IInboxRepository
{
    private readonly InventoryDbContext _dbContext;

    public InboxRepository(
        InventoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsAsync(
        Guid messageId,
        string consumer,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.InboxMessages
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