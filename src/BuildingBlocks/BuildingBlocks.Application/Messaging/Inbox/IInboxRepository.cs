namespace BuildingBlocks.Application.Messaging.Inbox
{
    public interface IInboxRepository
    {
        Task<bool> ExistsAsync(
        Guid messageId,
        string consumer,
        CancellationToken cancellationToken = default);

        Task AddAsync(
            InboxMessage message,
            CancellationToken cancellationToken = default);

        Task MarkProcessedAsync(
            Guid messageId,
            DateTime processedOnUtc,
            CancellationToken cancellationToken);
    }
}
