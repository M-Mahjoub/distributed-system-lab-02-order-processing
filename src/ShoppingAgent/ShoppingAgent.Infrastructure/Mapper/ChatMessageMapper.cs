using Microsoft.Extensions.AI;
using ShoppingAgent.Application.AI;
using ShoppingAgent.Domain;

namespace ShoppingAgent.Infrastructure.Mapper
{
    public static class ChatMessageMapper
    {
        public static ChatMessage ToMicrosoftMessage(
            ChatMessageDto message)
        {
            var contents = message.Contents != null ?
                message.Contents
                    .Select(MapContent)
                    .ToList() : new List<AIContent>();

            return new ChatMessage(
                MapRole(message.Role),
                contents);
        }

        private static ChatRole MapRole(
    MessageRole role)
        {
            return role switch
            {
                MessageRole.System =>
                    ChatRole.System,

                MessageRole.User =>
                    ChatRole.User,

                MessageRole.Assistant =>
                    ChatRole.Assistant,

                MessageRole.Tool =>
                    ChatRole.Tool,

                _ => throw new ArgumentOutOfRangeException(
                    nameof(role))
            };
        }

        private static AIContent MapContent(
    ChatContent content)
        {
            return content switch
            {
                TextChatContent text =>
                    new TextContent(text.Text),

                ToolCallChatContent toolCall =>
                    new FunctionCallContent(
                        toolCall.CallId,
                        toolCall.Name,
                        toolCall.Arguments.ToDictionary()),

                ToolResultChatContent toolResult =>
                    new FunctionResultContent(
                        toolResult.CallId,
                        toolResult.Result),

                _ => throw new ArgumentOutOfRangeException(
                    nameof(content))
            };
        }
    }
}
