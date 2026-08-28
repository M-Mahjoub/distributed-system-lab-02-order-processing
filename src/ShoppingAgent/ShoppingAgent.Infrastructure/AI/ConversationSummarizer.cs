using ShoppingAgent.Application.Abstractions;
using ShoppingAgent.Application.AI;
using ShoppingAgent.Domain;

namespace ShoppingAgent.Infrastructure.AI
{
    //خلاصه کردن
    public sealed class ConversationSummarizer
     : IConversationSummarizer
    {
        private readonly IChatModel _chatModel;

        public ConversationSummarizer(
            IChatModel chatModel)
        {
            _chatModel = chatModel;
        }

        public async Task<string> SummarizeAsync(
            IReadOnlyList<ChatMessageDto> messages,
            CancellationToken cancellationToken = default)
        {
            var prompt =
                """
            Summarize the following conversation.

            Preserve important information such as:
            - user's preferences
            - user's requirements
            - important decisions
            - products discussed
            - prices or budgets
            - unresolved questions

            Do not include unnecessary conversational text.

            Conversation:
            """;

            var summaryMessages =
                new List<ChatMessageDto>
                {
                new(
                    Guid.NewGuid(),
                    MessageRole.System,
                     new List<ChatContent> {
                        new TextChatContent(prompt)
                     })
                };

            foreach (var message in messages)
            {
                summaryMessages.Add(message);
            }

            var result = await _chatModel.GenerateAsync(
                summaryMessages,
                null,
               cancellationToken: cancellationToken);

            return result.Text;
        }
    }
}
