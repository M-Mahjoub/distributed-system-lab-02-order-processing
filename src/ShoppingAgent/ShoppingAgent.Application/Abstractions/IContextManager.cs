using ShoppingAgent.Application.AI;

namespace ShoppingAgent.Application.Memory;

public interface IContextManager
{
    IReadOnlyList<ChatMessageDto> BuildContext(
        Conversation conversation,
        WorkingMemory? workingMemory = null);
}