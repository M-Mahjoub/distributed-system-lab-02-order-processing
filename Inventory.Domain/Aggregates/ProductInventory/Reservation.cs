using BuildingBlocks.Domain.Common;

namespace Inventory.Domain.Aggregates.ProductInventory
{
    public sealed class Reservation : Entity<Guid>
    {
        public Guid OrderId { get; private set; }

        public int Quantity { get; private set; }

        private Reservation()
        {
        }

        public Reservation(
         Guid id,
         Guid orderId,
         int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(quantity));

            Id = id;
            OrderId = orderId;
            Quantity = quantity;
        }
    }
}
