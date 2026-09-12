using Microsoft.Extensions.AI;
using ShoppingAgent.Application.Abstractions;
using ShoppingAgent.Application.AI;
using ShoppingAgent.Domain;
using ShoppingAgent.Infrastructure.MCP;

namespace ShoppingAgent.Infrastructure.AI;

public sealed class OllamaChatModel : IChatModel
{
    private readonly IChatClient _chatClient;
    private readonly ShoppingMcpClient _mcpClient;

    public OllamaChatModel(
        IChatClient chatClient,
        ShoppingMcpClient mcpClient)
    {
        _chatClient = chatClient;
        _mcpClient = mcpClient;
    }

    public async Task<string> GenerateAsync(
        IEnumerable<ChatMessageDto> messages,
        CancellationToken cancellationToken = default)
    {
        // --------------------------------------------
        // 1. Get MCP tools
        // --------------------------------------------

        var tools =
            await _mcpClient.GetToolsAsync(
                cancellationToken);

        // --------------------------------------------
        // 2. Convert our ChatMessageDto
        //    to Microsoft.Extensions.AI ChatMessage
        // --------------------------------------------

        var chatMessages =
            messages
                .Select(MapMessage)
                .ToList();

        // --------------------------------------------
        // 3. Give MCP tools to the LLM
        // --------------------------------------------

        var options =
            new ChatOptions
            {
                Tools = [.. tools]
            };

        // --------------------------------------------
        // 4. Ask Qwen
        // --------------------------------------------

        var response =
            await _chatClient.GetResponseAsync(
                chatMessages,
                options,
                cancellationToken);

        return response.Text;
    }

    private static ChatMessage MapMessage(
        ChatMessageDto message)
    {
        var text =
            string.Join(
                "\n",
                message.Contents
                    .OfType<TextChatContent>()
                    .Select(x => x.Text));

        return new ChatMessage(
            MapRole(message.Role),
            text);
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

            _ =>
                ChatRole.User
        };
    }
}