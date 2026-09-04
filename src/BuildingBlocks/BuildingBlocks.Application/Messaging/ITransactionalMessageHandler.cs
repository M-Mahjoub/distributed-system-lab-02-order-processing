namespace BuildingBlocks.Application.Messaging;

public interface ITransactionalMessageHandler<in TMessage>
{
    Task HandleAsync(
        TMessage message,
        CancellationToken cancellationToken);
}