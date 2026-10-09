using ShoppingAgent.Application.Abstractions;
using ShoppingAgent.Application.Agents;
using ShoppingAgent.Application.AI;
using ShoppingAgent.Application.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAgent.Application.Services
{
    //مسئول چرخه‌ی LLM ↔ Tool باشد.
    public sealed class AgentService : IAgentService
    {
        private readonly IChatModel _chatModel;
        private readonly IContextManager _contextManager;
        private readonly IReadOnlyList<IAgentTool> _tools;

        public AgentService(
            IChatModel chatModel,
            IContextManager contextManager,
            IEnumerable<IAgentTool> tools)
        {
            _chatModel = chatModel;
            _tools = tools.ToList();
            _contextManager = contextManager;
        }

        public async Task<string> RunAsync(
    Conversation conversation,
     Guid messageId,
    string userMessage,
    CancellationToken cancellationToken = default)
        {
            conversation.AddUserMessage(messageId, userMessage);

            while (true)
            {
                var context =
                   _contextManager.BuildContext(
                       conversation);

              return
                    await _chatModel.GenerateAsync(
                        context,
                        cancellationToken);

                //if (response.ToolCalls.Count == 0)
                //{
                //    conversation.AddAssistantMessage(
                //        response.Text ?? string.Empty);

                //    return response.Text ?? string.Empty;
                //}

                //// Tool calls را اجرا می‌کنیم
                //foreach (var toolCall in response.ToolCalls)
                //{
                //    var tool =
                //        _tools.FirstOrDefault(x =>
                //            x.Name == toolCall.Name);

                //    if (tool is null)
                //    {
                //        throw new InvalidOperationException(
                //            $"Tool '{toolCall.Name}' not found.");
                //    }

                //    var result =
                //        await tool.ExecuteAsync(
                //            new Dictionary<string, object?>(
                //                toolCall.Arguments),
                //            cancellationToken);


                //    // اینجا باید Tool Result را
                //    // به Conversation برگردانیم.

                //    conversation.AddToolResult(
                //        toolCall.CallId,
                //        result);

                //}
            }
        }

        public async Task<string> RunAsync(
       IReadOnlyList<ChatMessageDto> context,
       CancellationToken cancellationToken = default)
        {
            return await _chatModel.GenerateAsync(
                context,
                cancellationToken);
        }
    }
}
