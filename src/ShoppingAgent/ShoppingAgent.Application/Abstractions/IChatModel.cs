using ShoppingAgent.Application.AI;

namespace ShoppingAgent.Application.Abstractions
{
    public interface IChatModel
    {
        Task<string> GenerateAsync(
            IEnumerable<ChatMessageDto> messages,
            CancellationToken cancellationToken = default);
    }
}
