using Microsoft.Extensions.AI;
using ShoppingAgent.Application.Abstractions;
using ShoppingAgent.Domain;
using ShoppingAgent.Application.AI;
using ShoppingAgent.Infrastructure.Tools;
using System.Linq;
using ShoppingAgent.Infrastructure.Mapper;

namespace ShoppingAgent.Infrastructure.AI
{
    public class OllamaChatModel : IChatModel
    {
        private readonly IChatClient _chatClient;

        public OllamaChatModel(
            IChatClient client)
        {
            _chatClient = client;
        }

        public async Task<ChatResponseDto> GenerateAsync(
     IEnumerable<ChatMessageDto> messages,
     IEnumerable<IAgentTool> tools,
     CancellationToken cancellationToken = default)
        {
            var chatMessages =
                messages.Select(Map).ToList();

            var aiTools =
                tools
                    .Select(AgentToolAdapter.ToAIFunction)
                    .ToList();
            try
            {

                var options = new ChatOptions
                {
                    Tools = aiTools.Cast<AITool>()
                                       .ToList()
                };



                var response =
                    await _chatClient.GetResponseAsync(
                        chatMessages,
                        options,
                        cancellationToken);

                var toolCalls =
                    response.Messages
                        .SelectMany(x => x.Contents)
                        .OfType<FunctionCallContent>()
                        .Select(x =>
                            new ChatToolCallDto(
                                x.CallId,
                                x.Name,
                                x.Arguments.AsReadOnly()))
                        .ToList();

                return new ChatResponseDto(
                    response.Text,
                    toolCalls);

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private static Microsoft.Extensions.AI.ChatMessage Map(
     ChatMessageDto message)
        {
            var role = message.Role switch
            {
                MessageRole.System =>
                    Microsoft.Extensions.AI.ChatRole.System,

                MessageRole.User =>
                    Microsoft.Extensions.AI.ChatRole.User,

                MessageRole.Assistant =>
                    Microsoft.Extensions.AI.ChatRole.Assistant,

                MessageRole.Tool =>
               Microsoft.Extensions.AI.ChatRole.Tool,

                _ => throw new ArgumentOutOfRangeException()
            };
            try
            {



                return new Microsoft.Extensions.AI.ChatMessage(
                    role,
                  ChatMessageMapper.ToMicrosoftMessage(message).Contents);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
