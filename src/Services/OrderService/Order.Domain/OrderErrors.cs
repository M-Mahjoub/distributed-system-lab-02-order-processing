using BuildingBlocks.Domain.Errors;

namespace Order.Domain
{
    public class OrderErrors
    {
        public static readonly Error InvalidId = new Error(
            "order.invalid.id",
            ErrorType.Validation);

        public static readonly Error CustomerId = new Error(
          "order.invalid.customerId",
          ErrorType.Validation);

        public static readonly Error ProductId = new Error(
         "order.invalid.productId",
         ErrorType.Validation);

        public static readonly Error OrderItemQuantityLessThanZero = new Error(
         "orderItem.invalid.quantity",
         ErrorType.Validation);

    }
}
