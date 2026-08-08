using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Infrastructure.Persistence.Outbox
{
    public class OutboxBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        //private readonly ILogger<OutboxBackgroundService> _logger;

        public OutboxBackgroundService(
            IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            //_logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope =
                        _scopeFactory.CreateScope();

                    var processor =
                        scope.ServiceProvider
                            .GetRequiredService<OutboxProcessor>();

                    await processor.ProcessAsync(
                        stoppingToken);
                }
                catch (Exception ex)
                {
                    //_logger.LogError(
                    //    ex,
                    //    "Error while processing outbox messages.");
                }

                await Task.Delay(
                    TimeSpan.FromSeconds(5),
                    stoppingToken);
            }
        }
    }
}
