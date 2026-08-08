using Inventory.Domain;
using Inventory.Domain.Abnstractions;
using Inventory.Infrastructure.Persistence.DbContexts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Inventory.Infrastructure.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        public InventoryDbContext _dbContext { get; set; }
        public InventoryRepository(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<IReadOnlyDictionary<ProductId, Domain.Aggregates.Inventory.Inventory>> GetByProductIdsAsync(
            IReadOnlyCollection<ProductId> ids,
            CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
