using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using ShoppingMcpServer.Clients;
using System.ComponentModel;

namespace ShoppingMcpServer.Tools;

//به MCP می‌گوید:

//این کلاس شامل MCP Toolها است.
[McpServerToolType]
public sealed class ProductTools
{

    private readonly ProductServiceClient _client;

    public ProductTools(
        ProductServiceClient client)
    {
        _client = client;
    }

    [McpServerTool]
    [Description(
        "Searches products by name or category.")]
    public async Task<object> SearchProducts(
        string query,
        CancellationToken cancellationToken = default)
    {
        return await _client.SearchAsync(
            query,
            cancellationToken);
    }

    ////این method یک Tool قابل فراخوانی از طریق MCP است.
    //[McpServerTool]
    //[Description(
    //    "Searches products by name or category.")]
    //public static object SearchProducts(
    //    string query)
    //{
    //    var products = new[]
    //    {
    //        new
    //        {
    //            Id = "p-001",
    //            Name = "Lenovo LOQ",
    //            Category = "Gaming Laptop",
    //            Price = 1399,
    //            Currency = "USD"
    //        },
    //        new
    //        {
    //            Id = "p-002",
    //            Name = "ASUS TUF Gaming",
    //            Category = "Gaming Laptop",
    //            Price = 1299,
    //            Currency = "USD"
    //        },
    //        new
    //        {
    //            Id = "p-003",
    //            Name = "Dell XPS 15",
    //            Category = "Laptop",
    //            Price = 1499,
    //            Currency = "USD"
    //        }
    //    };

    //    return products
    //        .Where(x =>
    //            x.Name.Contains(
    //                query,
    //                StringComparison.OrdinalIgnoreCase)
    //            ||
    //            x.Category.Contains(
    //                query,
    //                StringComparison.OrdinalIgnoreCase))
    //        .ToArray();
    //}

    
}