using ModelContextProtocol.Server;
using Microsoft.Extensions.AI;
using System.ComponentModel;

namespace ShoppingMcpServer.Prompts;

[McpServerPromptType]
public static class ProductPrompts
{
    [McpServerPrompt]
    [Description(
        "Creates a prompt for evaluating a product for purchase.")]
    public static ChatMessage EvaluateProduct(
        [Description("Product name")]
        string productName,

        [Description("Maximum budget in USD")]
        string budget)
    {
        return new ChatMessage(
            ChatRole.User,
            $"""
            Evaluate this product for a potential purchase.

            Product:
            {productName}

            Maximum budget:
            {budget} USD

            Consider:

            - Price
            - Product category
            - Value for money
            - Whether it fits the given budget

            Give a concise recommendation with reasons.
            """);
    }
}