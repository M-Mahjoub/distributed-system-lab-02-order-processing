using ShoppingAgent.Application.Abstractions;
using ShoppingAgent.Application.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAgent.Application.Services
{
    public sealed class AgentService
    {
        private readonly IChatModel _chatModel;
        private readonly IReadOnlyList<IAgentTool> _tools;

        public AgentService(
            IChatModel chatModel,
            IEnumerable<IAgentTool> tools)
        {
            _chatModel = chatModel;
            _tools = tools.ToList();
        }

        public async Task<string> RunAsync(
    Conversation conversation,
    string userMessage,
    CancellationToken cancellationToken = default)
        {
            conversation.AddUserMessage(userMessage);

            while (true)
            {
                var response =
                    await _chatModel.GenerateAsync(
                        conversation.Messages,
                        _tools,
                        cancellationToken);

                if (response.ToolCalls.Count == 0)
                {
                    conversation.AddAssistantMessage(
                        response.Text ?? string.Empty);

                    return response.Text ?? string.Empty;
                }

                // Tool calls را اجرا می‌کنیم
                foreach (var toolCall in response.ToolCalls)
                {
                    var tool =
                        _tools.FirstOrDefault(x =>
                            x.Name == toolCall.Name);

                    if (tool is null)
                    {
                        throw new InvalidOperationException(
                            $"Tool '{toolCall.Name}' not found.");
                    }

                    var result =
                        await tool.ExecuteAsync(
                            new Dictionary<string, object?>(
                                toolCall.Arguments),
                            cancellationToken);


                    // اینجا باید Tool Result را
                    // به Conversation برگردانیم.

                    conversation.AddToolResult(
                        toolCall.CallId,
                        result);

                }
            }
        }
    }
}
