using System.Threading.Tasks;

namespace Inventory.Domain.Abnstractions
{
    public interface IInventoryRepository
    {
        Task<IReadOnlyDictionary<ProductId, Inventory.Domain.Aggregates.Inventory.Inventory>>
            GetByProductIdsAsync(
                IReadOnlyCollection<ProductId> ids,
                CancellationToken cancellationToken);
    }
}
