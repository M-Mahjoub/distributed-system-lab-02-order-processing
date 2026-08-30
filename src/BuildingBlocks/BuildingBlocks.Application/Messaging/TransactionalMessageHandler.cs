using Inventory.Application.Abstractions.Persistence;

namespace BuildingBlocks.Application.Messaging;

public sealed class TransactionalMessageHandler<TMessage>
{
    private readonly ITransactionManager _transactionManager;
    private readonly IMessageHandler<TMessage> _handler;

    public TransactionalMessageHandler(
        ITransactionManager transactionManager,
        IMessageHandler<TMessage> handler)
    {
        _transactionManager = transactionManager;
        _handler = handler;
    }

    public Task HandleAsync<TResult>(
        TMessage message,
        CancellationToken cancellationToken)
    {
        return _transactionManager.ExecuteAsync(
            ct => _handler.HandleAsync(
                message,
                ct),
            cancellationToken);
    }
}