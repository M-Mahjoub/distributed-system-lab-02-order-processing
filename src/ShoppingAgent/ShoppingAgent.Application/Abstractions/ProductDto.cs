using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAgent.Application.Abstractions
{
    public sealed class ProductDto
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = default!;

        public decimal Price { get; init; }

        public string Currency { get; init; } = default!;
    }
}
