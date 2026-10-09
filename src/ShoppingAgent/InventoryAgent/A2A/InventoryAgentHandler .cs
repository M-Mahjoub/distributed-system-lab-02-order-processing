using A2A;
using InventoryAgent.MCP;
using System.Text.RegularExpressions;

namespace InventoryAgent;

//SDK درخواست A2A را به این handler می‌رساند.

public sealed class InventoryAgentHandler
    : IAgentHandler
{
    private readonly InventoryMcpClient _mcpClient;

    public InventoryAgentHandler(
        InventoryMcpClient mcpClient)
    {
        _mcpClient = mcpClient;
    }

    public async Task ExecuteAsync(
        RequestContext context,
        AgentEventQueue eventQueue,
        CancellationToken cancellationToken)
    {
        var productId =
            ExtractProductId(
                context.UserText);

        if (productId is null)
        {
            var responder =
                new MessageResponder(
                    eventQueue,
                    context.ContextId);

            await responder.ReplyAsync(
                "Please provide a product id.",
                cancellationToken);

            return;
        }

        var inventory =
            await _mcpClient.CheckInventoryAsync(
                productId,
                cancellationToken);

        var responder2 =
            new MessageResponder(
                eventQueue,
                context.ContextId);

        await responder2.ReplyAsync(
            inventory.ToString()!,
            cancellationToken);
    }

    private static string? ExtractProductId(
        string text)
    {
        var match =
            System.Text.RegularExpressions.Regex.Match(
                text,
                @"p-\d+",
                RegexOptions.IgnoreCase);

        return match.Success
            ? match.Value
            : null;
    }

    public static AgentCard GetAgentCard(
       string url)
    {
        return new AgentCard
        {
            Name = "Inventory Agent",

            Description =
                "An agent that handles inventory availability.",

            Version = "1.0.0",

            SupportedInterfaces =
            [
                new AgentInterface
                {
                    Url = url,
                    ProtocolBinding = "JSONRPC",
                    ProtocolVersion = "1.0"
                }
            ],

            DefaultInputModes =
            [
                "text/plain"
            ],

            DefaultOutputModes =
            [
                "text/plain"
            ],

            Capabilities =
                new AgentCapabilities
                {
                    Streaming = false
                },

            Skills =
            [
                new AgentSkill
                {
                    Id = "inventory-check",

                    Name =
                        "Inventory Check",

                    Description =
                        "Checks whether inventory is available.",

                    Tags =
                    [
                        "inventory",
                        "stock"
                    ]
                }
            ]
        };
    }
}