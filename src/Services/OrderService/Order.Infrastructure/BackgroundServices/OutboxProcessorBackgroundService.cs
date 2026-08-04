using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Order.Infrastructure.Messaging.IntegrationEvents;
using Order.Infrastructure.Persistence.DbContexts;
using System.Text.Json;

namespace Order.Infrastructure.BackgroundServices
{
    public class OutboxProcessorBackgroundService : BackgroundService
    {
        //چرا DbContext را Inject نکردیم؟ چون BackgroundService: singletone هست. ولی DbContext: scope
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IIntegrationEventTypeRegistry _registry;
        public OutboxProcessorBackgroundService(
            IIntegrationEventTypeRegistry registry,
            IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _registry = registry;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {

        //    public interface IOutboxProcessor
        //{
        //    Task ProcessAsync(
        //        OutboxMessage message,
        //        CancellationToken cancellationToken);
        //}
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope =
                          _scopeFactory.CreateScope();

                var db =
                    scope.ServiceProvider
                        .GetRequiredService<OrderDbContext>();

                var messages =
                    await db.OutboxMessages
                        .Where(x => x.ProcessedOnUtc == null)
                        .OrderBy(x => x.OccurredOnUtc)
                        .Take(20)
                        .ToListAsync(stoppingToken);

                foreach (var message in messages)
                {
                    var type =
                             _registry.Resolve(message.Type);
                    var integrationEvent =
                                           (IIntegrationEvent)
                                           JsonSerializer.Deserialize(
                                               message.Payload,
                                               type)!;
                    message.MarkAsProcessed();


                    try
                    {
                        //await publisher.PublishAsync(
                        //                   integrationEvent,
                        //                   stoppingToken);
                    }
                    catch (Exception ex)
                    {

                        message.MarkAsFailed(ex.Message);

                    }
                }

                await db.SaveChangesAsync(stoppingToken);

            }

        }
        //Every 5 seconds

        //↓

        //Read Outbox

        //↓

        //Deserialize

        //↓

        //RabbitMQ Publish

        //↓

        //Mark Processed

        //↓

        //Commit
    }
}
