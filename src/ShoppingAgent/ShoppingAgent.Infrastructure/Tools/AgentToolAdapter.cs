using Microsoft.Extensions.AI;
using ShoppingAgent.Application.Abstractions;

namespace ShoppingAgent.Infrastructure.Tools
{
    public static class AgentToolAdapter
    {
        public static AIFunction ToAIFunction(
            IAgentTool tool)
        {
            return AIFunctionFactory.Create(
                async () =>
                {
                    return await tool.ExecuteAsync(
                        new Dictionary<string, object?>());
                },
                tool.Name,
                tool.Description);
        }
    }
}
