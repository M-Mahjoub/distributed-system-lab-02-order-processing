using BuildingBlocks.Application.Messaging.Outbox;
using BuildingBlocks.Infrastructure.Messaging.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Infrastructure.Persistence.Outbox
{
    public sealed class OutboxProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OutboxProcessor> _logger;

        public OutboxProcessor(
            IServiceScopeFactory scopeFactory,
            ILogger<OutboxProcessor> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public async Task ProcessAsync(
                          CancellationToken cancellationToken)
        {
            using var scope =
                _scopeFactory.CreateScope();

            var dbContext =
                scope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();

            var publisher =
                scope.ServiceProvider
                    .GetRequiredService<IIntegrationEventPublisher>();

            var messages =
                await dbContext
                    .Set<OutboxMessage>()
                    .Where(x => x.ProcessedOnUtc == null)
                    .OrderBy(x => x.OccurredOnUtc)
                    .Take(50)
                    .ToListAsync(cancellationToken);

            foreach (var message in messages)
            {
                try
                {
                    await publisher.PublishAsync(message.Type,
                    message.Payload,
                    cancellationToken);

                    message.MarkAsProcessed();
                }
                catch (Exception ex)
                {
                    message.MarkAsFailed(ex.Message);
                }
            }

            await dbContext.SaveChangesAsync(
                cancellationToken);
        }
    }
}
