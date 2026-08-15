using ShoppingAgent.Application.Abstractions;

namespace ShoppingAgent.Application.AI
{
    public class FakeChatModel : IChatModel
    {
        public Task<string> GenerateAsync(
            string prompt,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(
                $"You said: {prompt}");
        }

        public Task<ChatResponseDto> GenerateAsync(IEnumerable<ChatMessageDto> messages, IEnumerable<IAgentTool> tools, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
