using BuildingBlocks.Domain.Errors;

namespace Inventory.Domain
{
    public class InventoryErrors
    {

        public static readonly Error InvalidId = new Error(
           "inventory.invalid.id",
           ErrorType.Validation);

        public static readonly Error InvalidQuantity = new Error(
          "inventory.invalidQuantity.quantity",
          ErrorType.Validation); 

        public static readonly Error InsufficientStock = new Error(
          "inventory.insufficientStock.availableQuantity",
          ErrorType.Validation);

        public static readonly Error StackIsNull = new Error(
         "inventory.stackisnull.stocks",
         ErrorType.Validation);

        public static readonly Error ReservationNotFound = new Error(
        "inventory.stackisnull.stocks",
        ErrorType.Validation);

        public static readonly Error AlreadyReserved = new Error(
        "inventory.stackisnull.stocks",
        ErrorType.Validation);

        public static Error ProductNotFound(ProductId productId) => new Error(
          $"inventory.productNotFound.{productId.Value.ToString()}",
          ErrorType.NotFound);
    }
}
