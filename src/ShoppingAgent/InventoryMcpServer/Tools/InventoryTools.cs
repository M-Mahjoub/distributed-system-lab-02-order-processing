using ModelContextProtocol.Server;
using System.ComponentModel;

namespace InventoryMcpServer.Tools;

[McpServerToolType]
public static class InventoryTools
{
    [McpServerTool]
    [Description(
        "Checks the available inventory quantity for a product.")]
    public static object CheckInventory(
        string productId)
    {
        var inventory = new[]
        {
            new
            {
                ProductId = "p-001",
                Quantity = 5
            },
            new
            {
                ProductId = "p-002",
                Quantity = 0
            },
            new
            {
                ProductId = "p-003",
                Quantity = 12
            }
        };

        var item =
            inventory.FirstOrDefault(
                x => x.ProductId == productId);

        if (item is null)
        {
            return new
            {
                ProductId = productId,
                Quantity = 0,
                Found = false
            };
        }

        return new
        {
            item.ProductId,
            item.Quantity,
            Found = true
        };
    }
}