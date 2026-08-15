using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAgent.Application.Abstractions
{
    public interface IProductSearch
    {
        Task<IReadOnlyList<ProductDto>> SearchAsync(
            string? query,
            decimal? maxPrice,
            CancellationToken cancellationToken = default);
    }
}
