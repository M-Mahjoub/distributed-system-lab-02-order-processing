using ModelContextProtocol.Server;
using ShoppingMcpServer.Clients;
using System.ComponentModel;

namespace ShoppingMcpServer.Resources;

[McpServerResourceType]
public sealed class ProductResources
{

    private readonly ProductServiceClient _client;

    public ProductResources(
        ProductServiceClient client)
    {
        _client = client;
    }

    [McpServerResource(UriTemplate =
        "products://{productId}")]
    [Description(
        "Returns detailed information about a product.")]
    public async Task<string> GetProduct(
        string productId,
        CancellationToken cancellationToken = default)
    {
        var product =
            await _client.GetByIdAsync(
                productId,
                cancellationToken);

        if (product is null)
        {
            return $"Product '{productId}' was not found.";
        }

        return $"""
        Product ID: {product.Id}
        Name: {product.Name}
        Category: {product.Category}
        Price: {product.Price} {product.Currency}
        """;
    }

    //[McpServerResource(UriTemplate = "products://catalog")]
    //[Description(
    //    "Provides the current product catalog.")]
    //public static string GetCatalog()
    //{
    //    return """
    //    Product Catalog

    //    p-001
    //    Name: Lenovo LOQ
    //    Category: Gaming Laptop
    //    Price: 1399 USD

    //    p-002
    //    Name: ASUS TUF Gaming
    //    Category: Gaming Laptop
    //    Price: 1299 USD

    //    p-003
    //    Name: Dell XPS 15
    //    Category: Laptop
    //    Price: 1499 USD
    //    """;
    //}

    //[McpServerResource(UriTemplate = "products://{productId}")]
    //[Description(
    //"Returns detailed information about a product.")]
    //public static string GetProduct(
    //string productId)
    //{
    //    var products = new Dictionary<string, object>
    //    {
    //        ["p-001"] = new
    //        {
    //            Id = "p-001",
    //            Name = "Lenovo LOQ",
    //            Category = "Gaming Laptop",
    //            Price = 1399,
    //            Currency = "USD"
    //        },

    //        ["p-002"] = new
    //        {
    //            Id = "p-002",
    //            Name = "ASUS TUF Gaming",
    //            Category = "Gaming Laptop",
    //            Price = 1299,
    //            Currency = "USD"
    //        },

    //        ["p-003"] = new
    //        {
    //            Id = "p-003",
    //            Name = "Dell XPS 15",
    //            Category = "Laptop",
    //            Price = 1499,
    //            Currency = "USD"
    //        }
    //    };

    //    if (!products.TryGetValue(
    //            productId,
    //            out var product))
    //    {
    //        return $"Product '{productId}' was not found.";
    //    }

    //    return product.ToString()!;
    //}
}