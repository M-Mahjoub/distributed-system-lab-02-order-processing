using ShoppingAgent.Application.Abstractions;
using ShoppingAgent.Application.AI;
using ShoppingAgent.Domain;

namespace ShoppingAgent.Infrastructure.AI
{
    public sealed class ContextManager : IContextManager
    {
        private const int MaxRecentMessages = 10;

        public IReadOnlyList<ChatMessageDto> BuildContext(
            Conversation conversation)
        {
            var result = new List<ChatMessageDto>();

            var systemMessages =
                conversation.Messages
                    .Where(x => x.Role == MessageRole.System);

            result.AddRange(systemMessages);

            if (!string.IsNullOrWhiteSpace(
                    conversation.Summary))
            {
                result.Add(
                    new ChatMessageDto(
                        MessageRole.System,
                        [
                            new TextChatContent(
                            $"""
                            Conversation summary:

                            {conversation.Summary}
                            """)
                        ]));
            }

            var recentMessages =
                conversation.Messages
                    .Where(x => x.Role != MessageRole.System)
                    .TakeLast(MaxRecentMessages);

            result.AddRange(recentMessages);

            return result;
        }
    }
}
