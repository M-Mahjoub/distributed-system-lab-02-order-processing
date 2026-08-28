using BuildingBlocks.Domain.Common;
using BuildingBlocks.Domain.Errors;
using Inventory.Domain.Aggregates.ProductInventory.Events;

namespace Inventory.Domain.Aggregates.ProductInventory
{
    public sealed class ProductInventory
        : AggregateRoot<Guid>
    {
        private readonly List<Reservation> _reservations = [];

        public Guid ProductId { get; private set; }

        public int AvailableQuantity { get; private set; }

        public int ReservedQuantity { get; private set; }

        public IReadOnlyCollection<Reservation> Reservations =>
            _reservations.AsReadOnly();

        private ProductInventory()
        {
        }

        public ProductInventory(
            Guid id,
            Guid productId,
            int availableQuantity)
            : base(id)
        {
            if (availableQuantity < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(availableQuantity));

            ProductId = productId;
            AvailableQuantity = availableQuantity;
        }

        public Result Reserve(
            Guid orderId,
            int quantity)
        {
            if (quantity <= 0)
            {
                return Result.Failure(
                    InventoryErrors.InvalidQuantity);
            }

            if (AvailableQuantity < quantity)
            {
                return Result.Failure(
                    InventoryErrors.InsufficientStock);
            }

            var existingReservation =
                _reservations.FirstOrDefault(
                    x => x.OrderId == orderId);

            if (existingReservation is not null)
            {
                return Result.Failure(
                    InventoryErrors.AlreadyReserved);
            }

            AvailableQuantity -= quantity;

            ReservedQuantity += quantity;

            _reservations.Add(
                new Reservation(
                    Guid.NewGuid(),
                    orderId,
                    quantity));

            Raise(
                new InventoryReservedDomainEvent(
                    Guid.NewGuid(),
                    DateTime.UtcNow,
                    orderId,
                    ProductId,
                    quantity));

            return Result.Success();
        }

        public Result Release(
            Guid orderId)
        {
            var reservation =
                _reservations.FirstOrDefault(
                    x => x.OrderId == orderId);

            if (reservation is null)
            {
                return Result.Failure(
                    InventoryErrors.ReservationNotFound);
            }

            _reservations.Remove(reservation);

            ReservedQuantity -= reservation.Quantity;

            AvailableQuantity += reservation.Quantity;

            Raise(
                new InventoryReleasedDomainEvent(
                    Guid.NewGuid(),
                    DateTime.UtcNow,
                    orderId,
                    ProductId,
                    reservation.Quantity));

            return Result.Success();
        }
    }
}
