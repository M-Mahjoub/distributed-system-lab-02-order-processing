using BuildingBlocks.Domain.Common;
using BuildingBlocks.Domain.Errors;
using Inventory.Domain.Aggregates.Inventory.Events;
using System.Runtime.InteropServices;

namespace Inventory.Domain.Aggregates.Inventory
{
    public class Inventory : AggregateRoot<InventoryId>
    {
        private Inventory()
        {

        }

        private Inventory(InventoryId inventoryId, ProductId productId, int reservedQuantity)
        {
            Id = inventoryId;
            ProductId = productId;
            ReservedQuantity = reservedQuantity;
        }
        public static Inventory Create(InventoryId inventoryId, ProductId productId, int reservedQuantity)
        {

            var inventory = new Inventory(inventoryId, productId, reservedQuantity);

            //inventory.Raise();

            return inventory;

        }
        public ProductId ProductId { get; }

        public int AvailableQuantity { get; private set; }

        public int ReservedQuantity { get; private set; }

        public uint Version { get; private set; }

        public Result Reserve(int quantity)
        {
            if (CanReserve(quantity))
            {
                AvailableQuantity -= quantity;
                ReservedQuantity += quantity;

                Raise(new InventoryReservedDomainEvent(
                                                      Id,
                                                      ProductId,
                                                      quantity));

                return Result.Success();
            }

            return Result.Failure(InventoryErrors.MoreQuantity);
        }

        public bool CanReserve(int quantity)
        {
            return AvailableQuantity >= quantity;
        }
    }
}
