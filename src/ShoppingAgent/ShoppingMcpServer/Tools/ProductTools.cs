using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace ShoppingMcpServer.Tools;

//به MCP می‌گوید:

//این کلاس شامل MCP Toolها است.
[McpServerToolType]
public static class ProductTools
{
    //این method یک Tool قابل فراخوانی از طریق MCP است.
    [McpServerTool]
    [Description(
        "Searches products by name or category.")]
    public static object SearchProducts(
        string query)
    {
        var products = new[]
        {
            new
            {
                Id = "p-001",
                Name = "Lenovo LOQ",
                Category = "Gaming Laptop",
                Price = 1399,
                Currency = "USD"
            },
            new
            {
                Id = "p-002",
                Name = "ASUS TUF Gaming",
                Category = "Gaming Laptop",
                Price = 1299,
                Currency = "USD"
            },
            new
            {
                Id = "p-003",
                Name = "Dell XPS 15",
                Category = "Laptop",
                Price = 1499,
                Currency = "USD"
            }
        };

        return products
            .Where(x =>
                x.Name.Contains(
                    query,
                    StringComparison.OrdinalIgnoreCase)
                ||
                x.Category.Contains(
                    query,
                    StringComparison.OrdinalIgnoreCase))
            .ToArray();
    }
}