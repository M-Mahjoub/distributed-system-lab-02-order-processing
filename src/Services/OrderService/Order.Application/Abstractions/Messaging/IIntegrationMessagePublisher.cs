namespace Order.Application.Abstractions.Messaging
{
    public interface IIntegrationMessagePublisher
    {
        Task PublishAsync<T>(
            T message,
            CancellationToken cancellationToken = default);
    }
}
