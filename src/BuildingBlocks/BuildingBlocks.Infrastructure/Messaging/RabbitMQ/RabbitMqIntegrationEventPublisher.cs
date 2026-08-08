namespace BuildingBlocks.Infrastructure.Messaging.RabbitMQ
{
    public class RabbitMqIntegrationEventPublisher : IIntegrationEventPublisher
    {
        public Task PublishAsync(string type, string payload, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
