using BuildingBlocks.Domain.Common;
using BuildingBlocks.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Aggregates.Inventory
{
    public sealed class Stock
    {
        public Guid ProductId { get; private set; }

        public int AvailableQuantity { get; private set; }

        public int ReservedQuantity { get; private set; }

        public Stock(
            Guid productId,
            int availableQuantity)
        {
            ProductId = productId;
            AvailableQuantity = availableQuantity;
        }

        public Result Reserve(int quantity)
        {
            if (quantity <= 0)
                return Result.Failure(
                    InventoryErrors.InvalidQuantity);

            if (AvailableQuantity < quantity)
                return Result.Failure(
                    InventoryErrors.InsufficientStock);

            AvailableQuantity -= quantity;
            ReservedQuantity += quantity;

            return Result.Success();
        }
    }
}
