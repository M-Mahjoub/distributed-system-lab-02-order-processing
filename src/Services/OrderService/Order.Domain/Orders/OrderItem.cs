using BuildingBlocks.Domain.Common;
using BuildingBlocks.Domain.Errors;

namespace Order.Domain.Orders;

public class OrderItem : Entity<Guid>
{
    public ProductId ProductId { get; set; }

    public decimal UnitPrice { get; set; }
    public string ProductNmae { get; set; }
    public int Quantity { get; set; }

    private OrderItem()
    {

    }

    public static OrderItem Create(ProductSnapshot product, int quantity)
    {
        return new OrderItem { ProductId = product.ProductId, Quantity = quantity };
    }

    public Result IncreaseQuantity(int quantity)
    {
        if (quantity <= quantity)
            return Result.Failure(OrderErrors.OrderItemQuantityLessThanZero);

        Quantity += quantity;

        return Result.Success();
    }
}
