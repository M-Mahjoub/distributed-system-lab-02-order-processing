using BuildingBlocks.Domain.Common;

namespace Inventory.Domain.Aggregates.Inventory
{

    public sealed class Inventory : Entity<Guid>
    {
        public Guid Id { get; protected init; }

        private Inventory()
        {
        }

        public Inventory(Guid id)
        {
            Id = id;
        }
    }
}
