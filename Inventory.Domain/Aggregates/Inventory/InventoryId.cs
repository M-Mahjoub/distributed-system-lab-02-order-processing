using BuildingBlocks.Domain.Errors;

namespace Inventory.Domain.Aggregates.Inventory
{
    public sealed record InventoryId(Guid Value)
    {
        public InventoryId New()
            => new InventoryId(Guid.CreateVersion7());

        public static Result<InventoryId> From(Guid value)
        {
            if (value == Guid.Empty)
                return Result.Failure<InventoryId>(InventoryErrors.InvalidId);

            return Result.Success(new InventoryId(value));
        }
    }
}
