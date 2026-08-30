namespace BuildingBlocks.Application.Messaging;

public interface IMessageWithId
{
    Guid MessageId { get; }
}