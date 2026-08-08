using BuildingBlocks.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain
{
    public sealed record ProductId(Guid Value)
    {
        public ProductId New()
            => new ProductId(Value);

        public static Result<ProductId> From(Guid value)
        {
            if (value == Guid.Empty)
                return Result.Failure<ProductId>(InventoryErrors.InvalidId);

            return Result.Success(new ProductId(value));
        }


    }
}
