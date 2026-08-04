using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain
{
    public sealed record ProductId(Guid Value)
    {
        public static ProductId New()
            => new(Guid.CreateVersion7());

        public static Result<ProductId> From(Guid value)
        {
            if (value == Guid.Empty)
                return Result.Failure<ProductId>(
                    OrderErrors.ProductId);

            return Result.Success(new ProductId(value));
        }

        public override string ToString()
         => Value.ToString();
    }

}
