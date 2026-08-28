using Inventory.Application.Abstractions.Persistence;
using Inventory.Domain.Aggregates.ProductInventory;
using Inventory.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.Repositories
{
    public sealed class ProductInventoryRepository
     : IProductInventoryRepository
    {
        private readonly InventoryDbContext _dbContext;

        public ProductInventoryRepository(
            InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductInventory?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.ProductInventories
                .Include(x => x.Reservations)
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<ProductInventory?> GetByProductIdAsync(
            Guid productId,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.ProductInventories
                .Include(x => x.Reservations)
                .FirstOrDefaultAsync(
                    x => x.ProductId == productId,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<ProductInventory>>
            GetByProductIdsAsync(
                IReadOnlyCollection<Guid> productIds,
                CancellationToken cancellationToken = default)
        {
            return await _dbContext.ProductInventories
                .Include(x => x.Reservations)
                .Where(x => productIds.Contains(x.ProductId))
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<ProductInventory>>
            GetByOrderIdAsync(
                Guid orderId,
                CancellationToken cancellationToken = default)
        {
            return await _dbContext.ProductInventories
                .Include(x => x.Reservations)
                .Where(x => x.Reservations
                    .Any(r => r.OrderId == orderId))
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(
            ProductInventory inventory,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.ProductInventories.AddAsync(
                inventory,
                cancellationToken);
        }
    }
}
