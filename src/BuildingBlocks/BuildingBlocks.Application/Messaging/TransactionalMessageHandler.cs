using BuildingBlocks.Application.Messaging.Inbox;
using Inventory.Application.Abstractions.Persistence;

namespace BuildingBlocks.Application.Messaging;

public sealed class TransactionalMessageHandler<TMessage>
    : ITransactionalMessageHandler<TMessage>
{
    private readonly ITransactionManager _transactionManager;
    private readonly IInboxRepository _inboxRepository;
    private readonly IMessageHandler<TMessage> _handler;
    private readonly IMessageIdAccessor<TMessage> _idAccessor;

    private readonly string _consumerName;

    public TransactionalMessageHandler(
        ITransactionManager transactionManager,
        IInboxRepository inboxRepository,
        IMessageHandler<TMessage> handler,
        IMessageIdAccessor<TMessage> idAccessor,
        string consumerName)
    {
        _transactionManager = transactionManager;
        _inboxRepository = inboxRepository;
        _handler = handler;
        _idAccessor = idAccessor;
        _consumerName = consumerName;
    }

    public Task HandleAsync(
        TMessage message,
        CancellationToken cancellationToken)
    {
        return _transactionManager.ExecuteAsync(
            async ct =>
            {
                var messageId =
                    _idAccessor.GetId(message);

                var exists =
                    await _inboxRepository.ExistsAsync(
                        messageId,
                        _consumerName,
                        ct);

                if (exists)
                    return;

                var inboxMessage =
                    new InboxMessage(
                        messageId,
                        _consumerName,
                        DateTime.UtcNow);

                await _inboxRepository.AddAsync(
                    inboxMessage,
                    ct);

                await _handler.HandleAsync(
                    message,
                    ct);

                inboxMessage.MarkAsProcessed(
                    DateTime.UtcNow);
            },
            cancellationToken);
    }
}