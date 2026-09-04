namespace BuildingBlocks.Application.Messaging.Outbox
{
    public interface IOutboxRepository
    {
        Task AddAsync(
            OutboxMessage message,
            CancellationToken cancellationToken = default);
    }
}
