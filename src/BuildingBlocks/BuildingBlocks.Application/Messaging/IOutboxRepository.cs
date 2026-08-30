namespace BuildingBlocks.Application.Messaging
{
    public interface IOutboxRepository
    {
        Task AddAsync(
            OutboxMessage message,
            CancellationToken cancellationToken = default);
    }
}
