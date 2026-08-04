using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain
{
    public sealed record CustomerId(Guid Value)
    {
        public static CustomerId New() => new(Guid.CreateVersion7());
        public static Result<CustomerId> From(Guid value)
        {
            if (value == Guid.Empty)
                return Result.Failure<CustomerId>(OrderErrors.CustomerId);

            return Result.Success<CustomerId>(new CustomerId(value));
        }
        public override string ToString() => Value.ToString();
    }
}
