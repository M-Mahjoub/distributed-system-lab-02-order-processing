using BuildingBlocks.Domain.Errors;

namespace Inventory.Domain
{
    public class InventoryErrors
    {

        public static readonly Error InvalidId = new Error(
           "inventory.invalid.id",
           ErrorType.Validation);

        public static readonly Error MoreQuantity = new Error(
          "inventory.moreQuantity.availableQuantity",
          ErrorType.Validation);

        public static Error ProductNotFound(ProductId productId) => new Error(
          $"inventory.productNotFound.{productId.Value.ToString()}",
          ErrorType.NotFound);
    }
}
