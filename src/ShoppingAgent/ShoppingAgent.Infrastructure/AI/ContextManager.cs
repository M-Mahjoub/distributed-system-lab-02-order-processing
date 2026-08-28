using ShoppingAgent.Application.Abstractions;
using ShoppingAgent.Application.AI;
using ShoppingAgent.Domain;

namespace ShoppingAgent.Infrastructure.AI
{
    //انتخاب Context مناسب
    public sealed class ContextManager : IContextManager
    {
        private const int MaxContextTokens = 4000;

        private readonly ITokenCounter _tokenCounter;

        public ContextManager(
            ITokenCounter tokenCounter)
        {
            _tokenCounter = tokenCounter;
        }

        public IReadOnlyList<ChatMessageDto> BuildContext(
            Conversation conversation)
        {
            var result = new List<ChatMessageDto>();

            var systemMessages =
                conversation.Messages
                    .Where(x => x.Role == MessageRole.System)
                    .ToList();

            result.AddRange(systemMessages);

            if (!string.IsNullOrWhiteSpace(
                    conversation.Summary))
            {
                result.Add(
                    new ChatMessageDto(
                        Guid.NewGuid(),
                        MessageRole.System,
                        [
                            new TextChatContent(
                            $"Conversation summary:\n{conversation.Summary}")
                        ]));
            }

            var usedTokens =
                result.Sum(
                    _tokenCounter.CountTokens);

            var selectedMessages =
                new List<ChatMessageDto>();

            var recentMessages =
                conversation.Messages
                    .Where(x =>
                        x.Role != MessageRole.System)
                    .Reverse();

            foreach (var message in recentMessages)
            {
                var messageTokens =
                    _tokenCounter.CountTokens(message);

                if (usedTokens + messageTokens >
                    MaxContextTokens)
                {
                    break;
                }

                selectedMessages.Add(message);

                usedTokens += messageTokens;
            }

            selectedMessages.Reverse();

            result.AddRange(selectedMessages);

            return result;
        }
    }
}
