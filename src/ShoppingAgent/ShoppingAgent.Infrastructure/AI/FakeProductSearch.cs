using ShoppingAgent.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAgent.Infrastructure.AI
{
    public sealed class FakeProductSearch : IProductSearch
    {
        private readonly List<ProductDto> _products =
        [
            new()
        {
            Id = Guid.NewGuid(),
            Name = "Dell XPS 15",
            Price = 45000000,
            Currency = "IRR"
        },

        new()
        {
            Id = Guid.NewGuid(),
            Name = "Lenovo ThinkPad T14",
            Price = 38000000,
            Currency = "IRR"
        },

        new()
        {
            Id = Guid.NewGuid(),
            Name = "MacBook Pro",
            Price = 80000000,
            Currency = "IRR"
        }
        ];

        public Task<IReadOnlyList<ProductDto>> SearchAsync(
            string? query,
            decimal? maxPrice,
            CancellationToken cancellationToken = default)
        {
            var result = _products
                .Where(x =>
                    query is null ||
                    x.Name.Contains(
                        query,
                        StringComparison.OrdinalIgnoreCase))
                .Where(x =>
                    maxPrice is null ||
                    x.Price <= maxPrice)
                .ToList();

            return Task.FromResult<
                IReadOnlyList<ProductDto>>(result);
        }
    }
}
