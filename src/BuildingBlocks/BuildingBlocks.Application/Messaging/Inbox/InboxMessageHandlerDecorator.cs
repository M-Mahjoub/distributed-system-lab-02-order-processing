using BuildingBlocks.Application.Abstractions.Persistence;

namespace BuildingBlocks.Application.Messaging.Inbox;

public sealed class InboxMessageHandlerDecorator<TMessage>
    : IMessageHandler<TMessage>
{
    private readonly IInboxRepository _inboxRepository;
    private readonly IMessageHandler<TMessage> _inner;

    private readonly string _consumerName;

    public InboxMessageHandlerDecorator(
        IInboxRepository inboxRepository,
        IMessageHandler<TMessage> inner,
        string consumerName)
    {
        _inboxRepository = inboxRepository;
        _inner = inner;
        _consumerName = consumerName;
    }

    public async Task HandleAsync(
        TMessage message,
        CancellationToken cancellationToken)
    {
        // این قسمت نیاز به استخراج MessageId دارد.
        // فعلاً برای سادگی فرض می‌کنیم IMessageWithId داریم.

        var messageWithId =
            (IMessageWithId)message;

        var exists =
            await _inboxRepository.ExistsAsync(
                messageWithId.MessageId,
                _consumerName,
                cancellationToken);

        if (exists)
            return;

        var inbox =
            new InboxMessage(
                messageWithId.MessageId,
                _consumerName,
                DateTime.UtcNow);

        await _inboxRepository.AddAsync(
            inbox,
            cancellationToken);

        await _inner.HandleAsync(
            message,
            cancellationToken);

        inbox.MarkAsProcessed(
            DateTime.UtcNow);

    }
}