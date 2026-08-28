using Inventory.Domain.Aggregates.ProductInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Abstractions.Persistence
{
    public interface IProductInventoryRepository
    {
        Task<ProductInventory?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<ProductInventory?> GetByProductIdAsync(
            Guid productId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ProductInventory>>
            GetByProductIdsAsync(
                IReadOnlyCollection<Guid> productIds,
                CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ProductInventory>>
            GetByOrderIdAsync(
                Guid orderId,
                CancellationToken cancellationToken = default);

        Task AddAsync(
            ProductInventory inventory,
            CancellationToken cancellationToken = default);
    }
}
