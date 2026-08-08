using BuildingBlocks.Domain.Errors;

namespace Order.Domain.Orders;

public sealed record OrderId(Guid Value)
{
    public static OrderId New()
        => new(Guid.CreateVersion7());

    public static Result<OrderId> From(Guid value)
    {
        if (value == Guid.Empty)
            return Result.Failure<OrderId>(
                OrderErrors.InvalidId);

        return Result.Success(new OrderId(value));
    }

    public override string ToString()
     => Value.ToString();
}
