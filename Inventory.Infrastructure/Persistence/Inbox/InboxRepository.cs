using BuildingBlocks.Infrastructure.Persistence.Inbox;
using Inventory.Application.Abstractions.Messaging;
using Inventory.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.Inbox
{
    public sealed class InboxRepository : IInboxRepository
    {
        private readonly InventoryDbContext _dbContext;

        public InboxRepository(
            InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> ExistsAsync(
            Guid messageId,
            CancellationToken cancellationToken)
        {
            return _dbContext.InboxMessages
                .AnyAsync(
                    x => x.MessageId == messageId,
                    cancellationToken);
        }

        public async Task AddAsync(
            Guid messageId,
            string type,
            DateTime receivedOnUtc,
            CancellationToken cancellationToken)
        {
            var message = new InboxMessage
            {
                MessageId = messageId,
                Type = type,
                ReceivedOnUtc = receivedOnUtc
            };

            await _dbContext.InboxMessages.AddAsync(
                message,
                cancellationToken);
        }

        public async Task MarkProcessedAsync(
            Guid messageId,
            DateTime processedOnUtc,
            CancellationToken cancellationToken)
        {
            var message =
                await _dbContext.InboxMessages
                    .FirstAsync(
                        x => x.MessageId == messageId,
                        cancellationToken);

            message.ProcessedOnUtc = processedOnUtc;
        }
    }
}
